using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string InsertReadingService(int saveReading);

        //[OperationContract]
        //Reading GetReadingService(int getReading);

        //[OperationContract]
        //IEnumerable<Reading> GetReadingsService();
    }


    [DataContract]
    public class Reading
    {
        [DataMember]
        public int Value { get; set; }

        public Reading(int value)
        {
            this.Value = value;
        }

        public Reading()
        {
        }
    }
}
    
