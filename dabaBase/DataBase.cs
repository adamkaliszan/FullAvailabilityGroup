using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using ModelGroup;
using Algorithms;

namespace BazaDanych
{
    public class DataBase : IBDwynsymulacji
    {
        Dictionary<trClass, int?> _slownikKlasaIndeks;
        int _idSystem;

        DataTable _wynikiSystemu;
        int _idSystemuZwynikami;


        readonly System.Globalization.CultureInfo culture;
        MySqlConnection dbConnection;
        Wiazka.Debug debug;
        Wiazka.Debug error;

        string plikParSymulacji = "parSymulacji.xml";
        string plikSystemy = "systemy.xml";

        public DataBase(string cs, Wiazka.Debug debug, Wiazka.Debug error)
        {
            culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

            this.debug = debug;
            this.error = error;

            dbConnection = new MySqlConnection(cs);

            _slownikKlasaIndeks = new Dictionary<trClass, int?>();
            _idSystem = -1;

            _wynikiSystemu = null;
            _idSystemuZwynikami = -1;
        }

        public List<DataBaseSimulPar> dodajPredefiniowaneParametrySymulacji()
        {
            List<DataBaseSimulPar> lista = new List<DataBaseSimulPar>();
            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);

                DataTable tabelaParSymulacji = new DataTable("parametrySymulacji");

                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM parametrySymulacji", dbConnection))
                    {
                        adapter.Fill(tabelaParSymulacji);
                    }

                    if (!byloOtwarte)
                        dbConnection.Close();
                    tabelaParSymulacji.WriteXml(plikParSymulacji, XmlWriteMode.WriteSchema);
                }
                catch
                {
                    error("Błąd wczytania parametrów synulacji z bazy danych");
                    tabelaParSymulacji.ReadXml(plikParSymulacji);
                }


                foreach (DataRow tmp in tabelaParSymulacji.Rows)
                    lista.Add(new DataBaseSimulPar(tmp));
            }
            return lista;
        }

        public List<BDsystem> dodajPredefiniowaneSystemy()
        {
            List<BDsystem> lista = new List<BDsystem>();
            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);

                DataTable tabelaSystemy = new DataTable("systemy");

                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM systemy", dbConnection))
                    {
                        adapter.Fill(tabelaSystemy);
                    }

                    if (!byloOtwarte)
                        dbConnection.Close();
                    tabelaSystemy.WriteXml(plikSystemy, XmlWriteMode.WriteSchema);
                }
                catch
                {
                    error("Błąd wczytania predefiniowanych systemów z bazy danych");
                    tabelaSystemy.ReadXml(plikSystemy);
                }

                foreach (DataRow tmp in tabelaSystemy.Rows)
                    lista.Add(new BDsystem(tmp));
            }
            return lista;
        }

        public bool wczytajKlasyZgloszen(BDsystem system, Wiazka mSystemu)
        {
            int id = system.id;
            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);

                system.klasy.Clear();

                DataTable tabelaKlasy = new DataTable("systemy");
                lock (dbConnection)
                {
                    try
                    {
                        if (!byloOtwarte)
                            dbConnection.Open();

                        string polecenie = string.Format(culture, "SELECT * FROM klasy WHERE idSystem = {0}", system.id);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(polecenie, dbConnection))
                        {
                            adapter.Fill(tabelaKlasy);

                            foreach (DataRow tmp in tabelaKlasy.Select())
                            {
                                string strAT = tmp["propAT"].ToString();
                                string strT = tmp["t"].ToString();
                                string strMu = tmp["mu"].ToString();
                                string strS = tmp["S"].ToString();
                                string strUprzywilejowana = tmp["uprzywilejowana"].ToString();
                                string strTyp = tmp["typ"].ToString();

                                trClass nowaKlasa = null;
                                switch (strTyp)
                                {
                                    case "Erlang":
                                        nowaKlasa = new trClassErlang(mSystemu, int.Parse(strAT), int.Parse(strT), double.Parse(strMu), bool.Parse(strUprzywilejowana));
                                        break;
                                    case "Engset":
                                        nowaKlasa = new trClassEngset(mSystemu, int.Parse(strAT), int.Parse(strT), double.Parse(strMu), bool.Parse(strUprzywilejowana), int.Parse(strS));
                                        break;
                                    case "Pascal":
                                        nowaKlasa = new trClassPascal(mSystemu, int.Parse(strAT), int.Parse(strT), double.Parse(strMu), bool.Parse(strUprzywilejowana), int.Parse(strS));
                                        break;
                                }
                                system.klasy.Add(new BDklasaZgl(id, nowaKlasa));
                            }
                        }

                        if (!byloOtwarte)
                            dbConnection.Close();
                        tabelaKlasy.WriteXml(plikSystemy, XmlWriteMode.WriteSchema);
                    }
                    catch
                    {
                        error(string.Format("Błąd dodawania klas zgłoszeń dla systemu id {0}", id));
                        return false;
                    }
                }
            }
            return true;
        }

         
        public int wynikDla(double a, int nrSerii, int parSymulacji, int algSymulacji, Wiazka mWiazka)
        {
            if (_idSystemuZwynikami != mWiazka.sysNo)
                pobierzWynikiDlaSystemu(mWiazka.sysNo);

            int wynik = 0;
            foreach (trClass klasa in mWiazka.ListaKlasRuchu)
            {
                int idKlasy = odczytajIdKlasy(mWiazka.sysNo, klasa);

                string filtr = string.Format(culture, "a = {0} AND nrSerii = {1} AND idParSymulacji = {2} AND idKlasy = {3} AND idMetSymulacji = {4}", a, nrSerii, parSymulacji, idKlasy, algSymulacji);
                DataRow[] tabelaWyniki = _wynikiSystemu.Select(filtr);
                if (tabelaWyniki.Length > 0)
                    wynik++;

            }
            return wynik;
        }

        private void pobierzWynikiDlaSystemu(int nrSystemu)
        {
            _idSystemuZwynikami = nrSystemu;

            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == ConnectionState.Open);

                _wynikiSystemu = new DataTable("wynikiSymulacji");
                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    string polecenie = string.Format(culture, "SELECT * FROM wynikiSymulacji WHERE idSystem = {0}", nrSystemu);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(polecenie, dbConnection))
                    {
                        adapter.Fill(_wynikiSystemu);
                    }

                    if (!byloOtwarte)
                        dbConnection.Close();
                }
                catch (Exception e)
                {
                    string komunikatBledu = string.Format("pobierzWynikiDlaSystemu: wyjątek {0}", e);
                }
            }
        }

        private int odczytajIdKlasy(int idSystem, trClass klasa)
        {
            if (_idSystem != idSystem)
                _slownikKlasaIndeks.Clear();
            _idSystem = idSystem;

            if (_slownikKlasaIndeks.ContainsKey(klasa))
                return (int)_slownikKlasaIndeks[klasa];

            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);
                int wynik = 0;
                DataTable tabelaKlasy = new DataTable("klasy");
                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    string typ = "";
                    switch (klasa.typ)
                    {
                        case trClass.typKlasy.ERLANG: typ = "Erlang"; break;
                        case trClass.typKlasy.ENGSET: typ = "Engset"; break;
                        case trClass.typKlasy.PASCAL: typ = "Pascal"; break;
                    }
                    //string polecenie = string.Format(culture, "SELECT * FROM klasy WHERE idSystem = {0} AND propAT = {1} AND t = {2} AND mu = {3} AND typ = '{4}' AND S = {4} AND uprzywilejowana = {5} AND progi = {6}", idSystem, klasa.atProp, klasa.t, klasa.mu, typ, klasa.S, klasa.uprzywilejowana, klasa.progiKlasy);
                    string polecenie = string.Format(culture, "SELECT * FROM klasy WHERE idSystem = {0}", idSystem, klasa.atProp, klasa.t, klasa.mu, typ, klasa.S, klasa.uprzywilejowana, klasa.progiKlasy);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(polecenie, dbConnection))
                    {
                        adapter.Fill(tabelaKlasy);
                        if (tabelaKlasy.Rows.Count > 0)
                        {
                            string filtr = string.Format(culture, "propAT = {0} AND t = {1} AND mu = {2} AND typ = '{3}' AND S = {4} AND uprzywilejowana = {5} AND progi = {6}", klasa.atProp, klasa.t, klasa.mu, typ, klasa.S, klasa.uprzywilejowana, (klasa.progiKlasy == null) ? false : true);
                            DataRow[] pasujaceKlasy = tabelaKlasy.Select(filtr);
                            if (pasujaceKlasy.Length != 1)
                            {
                                error(string.Format("Znaleziono {2} klas {1} dla systemu {0}", idSystem, klasa, pasujaceKlasy.Length));
                                return -pasujaceKlasy.Length;
                            }
                            wynik = int.Parse(pasujaceKlasy[0]["id"].ToString());
                        }
                        else
                        {
                            error(string.Format("Nie znaleziono klas dla systemu {0}", idSystem));
                            return -1;
                        }
                    }

                    if (!byloOtwarte)
                        dbConnection.Close();
                }
                catch (Exception e)
                {
                    string tekstBledu = string.Format("odczytajIdKlasy: przechwycono  wyjątek {0}", e);
                    error(tekstBledu);
                    return 0;
                }

                _slownikKlasaIndeks.Add(klasa, wynik);
                return wynik;
            }
        }

        public bool odczytEB(double a, int nrSerii, int parSymulacji, int algSymulacji, int nrSystemu, trClass klasa, out double E, out double B)
        {
            if (_idSystemuZwynikami != nrSystemu)
                pobierzWynikiDlaSystemu(nrSystemu);

            int idKlasy = odczytajIdKlasy(nrSystemu, klasa);

            try
            {
                string filtr = string.Format(culture, "a = {0} AND nrSerii = {1} AND idParSymulacji = {2} AND idMetSymulacji = {4} AND idKlasy = {5}", a, nrSerii, parSymulacji, nrSystemu, algSymulacji, idKlasy);

                DataRow []tabelaWyniki = _wynikiSystemu.Select(filtr);

                E = double.Parse(tabelaWyniki[0]["blokada"].ToString());
                B = double.Parse(tabelaWyniki[0]["straty"].ToString());
                
            }
            catch (Exception e)
            {
                string komunikatBledu = string.Format("odczytEB: wyjątek {0}", e); 

                E = -1;
                B = -1;
                return false;
            }
            return true;
        }

        public bool zapisEB(double a, int nrSerii, int parSymulacji, int algSymulacji, int nrSystemu, trClass klasa, double wartoscE, double wartoscB)
        {
            int idKlasy = odczytajIdKlasy(nrSystemu, klasa);


            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);

                DataTable tabelaWyniki = new DataTable("wynikiSymulacji");
                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    string polecenie = string.Format(culture, "INSERT INTO wynikiSymulacji (a, nrSerii, idParSymulacji, idSystem, idMetSymulacji, idKlasy, blokada, straty) VALUES  ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", a, nrSerii, parSymulacji, nrSystemu, algSymulacji, idKlasy, wartoscE, wartoscB);
                    MySqlCommand cmd = new MySqlCommand(polecenie, dbConnection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    return false;
                }
            }

            return true;
        }

        public void addNewSystem(Wiazka mWiazka)
        {
            lock (dbConnection)
            {
                bool byloOtwarte = (dbConnection.State == System.Data.ConnectionState.Open);

                try
                {
                    if (!byloOtwarte)
                        dbConnection.Open();

                    double[] v = new double[BDsystem.maxLpodgrup];
                    double[] k = new double[BDsystem.maxLpodgrup];

                    for (int i = 0; i < BDsystem.maxLpodgrup; i++)
                    {
                        if (mWiazka.ListaPodgrupLaczy.Count > i)
                        {
                            v[i] = mWiazka.ListaPodgrupLaczy[i].v;
                            k[i] = mWiazka.ListaPodgrupLaczy[i].k;
                        }
                        else
                        {
                            v[i] = 0;
                            k[i] = 0;
                        }
                    }

                    string strTypRezerwacji;
                    switch (mWiazka.AlgorytmRezerwacji)
                    {
                        case reservationAlgorithm.none:
                            strTypRezerwacji = "brak";
                            break;
                        case reservationAlgorithm.R1_R2:
                            strTypRezerwacji = "r1r2";
                            break;
                        case reservationAlgorithm.R3:
                            strTypRezerwacji = "r3";
                            break;
                        default:
                            strTypRezerwacji = "x";
                            break;
                    }

                    string strAlgWybPodgr;
                    switch (mWiazka.aWybPodgr)
                    {
                        case subgroupChooseAlgorithm.random:
                            strAlgWybPodgr = "losowy";
                            break;
                        case subgroupChooseAlgorithm.randomCapacityProportional:
                            strAlgWybPodgr = "losowyPropDoPojPodgr";
                            break;
                        case subgroupChooseAlgorithm.randomOccupancyProportional:
                            strAlgWybPodgr = "losowyPropDoStanu";
                            break;
                        case subgroupChooseAlgorithm.sequence:
                            strAlgWybPodgr = "kolejnosciowy";
                            break;
                        case subgroupChooseAlgorithm.RR:
                            strAlgWybPodgr = "cykliczny";
                            break;
                        default:
                            strAlgWybPodgr = "x";
                            break;
                    }

                    string polecenie = string.Format("INSERT INTO systemy (V, k, m, k1, v1, k2, v2, k3, v3, typRezerwacji, grRezerwacji, algWybPodgr) VALUES  (@V, @k, @m, @k1, @v1, @k2, @v2, @k3, @v3, @typRezerwacji, @grRezerwacji, @algWybPodgr)");
                    using (MySqlCommand cmd = new MySqlCommand(polecenie, dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@V", mWiazka.V);
                        cmd.Parameters.AddWithValue("@k", mWiazka.sumaK);
                        cmd.Parameters.AddWithValue("@m", mWiazka.m);
                        cmd.Parameters.AddWithValue("@k1", k[0]);
                        cmd.Parameters.AddWithValue("@v1", v[0]);
                        cmd.Parameters.AddWithValue("@k2", k[1]);
                        cmd.Parameters.AddWithValue("@v2", v[1]);
                        cmd.Parameters.AddWithValue("@k3", k[2]);
                        cmd.Parameters.AddWithValue("@v3", v[2]);

                        cmd.Parameters.AddWithValue("@typRezerwacji", strTypRezerwacji);
                        cmd.Parameters.AddWithValue("@grRezerwacji", mWiazka.q);
                        cmd.Parameters.AddWithValue("@algWybPodgr", strAlgWybPodgr);

                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT @@IDENTITY";
                        cmd.Parameters.Clear();
                        mWiazka.sysNo = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    polecenie = string.Format("INSERT INTO klasy (idSystem, propAT, t, mu, typ, S, uprzywilejowana, progi) VALUES  (@idSystem, @propAT, @t, @mu, @typ, @S, @uprzywilejowana, @progi)");
                    foreach (trClass klasa in mWiazka.ListaKlasRuchu)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(polecenie, dbConnection))
                        {
                            cmd.Parameters.AddWithValue("@idSystem", mWiazka.sysNo);
                            cmd.Parameters.AddWithValue("@propAT", klasa.atProp);
                            cmd.Parameters.AddWithValue("@t", klasa.t);
                            cmd.Parameters.AddWithValue("@mu", klasa.mu);
                            if (klasa is trClassErlang)
                                cmd.Parameters.AddWithValue("@typ", "Erlang");
                            else if (klasa is trClassEngset)
                                cmd.Parameters.AddWithValue("@typ", "Engset");
                            else if (klasa is trClassPascal)
                                cmd.Parameters.AddWithValue("@typ", "Pascal");

                            cmd.Parameters.AddWithValue("@S", klasa.S);
                            cmd.Parameters.AddWithValue("@uprzywilejowana", klasa.uprzywilejowana);
                            cmd.Parameters.AddWithValue("@progi", (klasa.progiKlasy != null));

                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteScalar();
                        }
                    }
                }
                catch (Exception e)
                {
                    error(string.Format("DodajNowySystem: {0}", e));
                }
                finally
                {
                    if (!byloOtwarte)
                        dbConnection.Close();

                }
            }
        }
    }
}
