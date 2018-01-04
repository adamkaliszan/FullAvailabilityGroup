using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ModelGroup;

using MySql;
using MySql.Data.MySqlClient;

namespace BazaDanych
{
    public class BDsystem
    {
        int _id;
        int _V;
        int _k;
        int _m;
        int _q;

        ModelGroup.reservationAlgorithm _algRez = reservationAlgorithm.none;
        ModelGroup.subgroupChooseAlgorithm _algWybPodgr = subgroupChooseAlgorithm.random;


        public List<BDklasaZgl> klasy;

        public int id { get { return _id; } }
        public int V { get { return _V; } }
        public int k { get { return _k; } }
        public int m { get { return _m; } }
        public int q { get { return _q; } }

        public ModelGroup.reservationAlgorithm algRez { get { return _algRez; } }
        public ModelGroup.subgroupChooseAlgorithm algWybPodgr { get { return _algWybPodgr; } }
        
        public const int maxLpodgrup = 3;
        public BDlacze []podgrupy; 

        public BDsystem(DataRow rekord)
        {
            _id = int.Parse(rekord["id"].ToString());
            _V = int.Parse(rekord["V"].ToString());
            _k = int.Parse(rekord["k"].ToString());
            _m = int.Parse(rekord["m"].ToString());
            _q = int.Parse(rekord["grRezerwacji"].ToString());
            
            switch (rekord["typRezerwacji"].ToString())
            {
                case "brak":
                    _algRez = reservationAlgorithm.none;
                    break;
                case "r1r2":
                    _algRez = reservationAlgorithm.R1_R2;
                    break;
                case "r3":
                    _algRez = reservationAlgorithm.R3;
                    break;
            }


            switch (rekord["algWybPodgr"].ToString())
            {
                case "losowy":
                    _algWybPodgr = subgroupChooseAlgorithm.random;
                    break;
                case "losowyPropDoPojPodgr":
                    _algWybPodgr = subgroupChooseAlgorithm.randomCapacityProportional;
                    break;
                case "losowyPropDoStanu":
                    _algWybPodgr = subgroupChooseAlgorithm.randomOccupancyProportional;
                    break;
                case "kolejnosciowy":
                    _algWybPodgr = subgroupChooseAlgorithm.sequence;
                    break;
                case "cykliczny":
                    _algWybPodgr = subgroupChooseAlgorithm.RR;
                    break;
            }

            klasy = new List<BDklasaZgl>();

            podgrupy = new BDlacze[maxLpodgrup];

            for (int nrPodgr = 0; nrPodgr < maxLpodgrup; nrPodgr++)
            {
                string str_k = rekord[string.Format("k{0}", nrPodgr+1)].ToString();
                string str_v = rekord[string.Format("v{0}", nrPodgr+1)].ToString();
                podgrupy[nrPodgr] = new BDlacze(int.Parse(str_v), int.Parse(str_k));
            }
        }

        public override string ToString()
        {
            string wynik = string.Format("{0} V={1}, k={2}, m={3}", id, V, k, m);
            if (k > 1)
                wynik += string.Format(" alg. wyb. podgr. {0}", algWybPodgr);
            if (algRez != reservationAlgorithm.none)
                wynik += string.Format(" alg. rez. {0}", algRez);
            return wynik;
        }
    }

    public class BDlacze
    {
        int _v;
        int _k;

        public int v {get {return  _v;}}
        public int k {get {return _k;}}

        public BDlacze(int v, int k)
        {
            _v = v;
            _k = k;
        }
    }

    public class BDklasaZgl
    {
        trClass _klasaZgl;
        int _id;

        public trClass klasaZgl { get { return _klasaZgl; } }
        public int id { get { return _id; } }

        public BDklasaZgl(int id, trClass klasa)
        {
            _id = id;
            _klasaZgl = klasa;
        }
    }
}
