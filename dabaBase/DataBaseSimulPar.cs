using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BazaDanych
{
    public class DataBaseSimulPar
    {
        int _lSerii;
        int _lUtrZgl;
        double _czStartu;
        int _id;
        string _opis;

        public int lSeri { get { return _lSerii; } }
        public int lUtrZgl { get { return _lUtrZgl; } }
        public double czStartu { get { return _czStartu; } }
        public int id { get { return _id; } }

        public DataBaseSimulPar(DataRow rekord)
        {
            _id = int.Parse(rekord["id"].ToString());
            _lSerii = int.Parse(rekord["lSerii"].ToString());
            _lUtrZgl = int.Parse(rekord["lUtrZgl"].ToString());
            _czStartu = UInt64.Parse(rekord["fazaPrzejsciowa"].ToString());
            _opis = rekord["Opis"].ToString();
        }

        public override string ToString()
        {
            return string.Format("{3}) {0} {1} {2}", _lSerii, _lUtrZgl, _czStartu, id);
        }
    }
}
