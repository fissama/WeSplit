using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplit.Model
{
    class JourneyModel
    {
        public int JId { get; set; }
        public String JName { get; set; }
        public String nameDes { get; set; }
        public String infoDes { get; set; }
        public String imgPath { get; set; }
        public String JIntroduce { get; set; }
        public bool JStatus { get; set; }
        public String Jstatus { get; set; }
        public DateTime JDayStart { get; set; }
        public DateTime JDayEnd { get; set; }

        public List<MemberModel> JMembers = new List<MemberModel>();
        public List<ExpenditureModel> JExpenditure = new List<ExpenditureModel>();
        public List<String> JImage = new List<String>();

    }
}
