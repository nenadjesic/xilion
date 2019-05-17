using Xilion.Models.Core.Data;
using Xilion.Models.Core.Domain;
using Xilion.Models.Site;

namespace Xilion.Models.GenericContent
{

    public class GenericContent : MetaDataEntity
    {
   
        public virtual string Title
        {
            get { return MetaData.GetValue<string>("Title"); }
            set { MetaData.SetValueNull("Title", value); }
        }

        public virtual string Content
        {
            get { return MetaData.GetValue<string>("Content"); }
            set { MetaData.SetValueNull("Content", value); }
        }


        public virtual Page Page { get; set; }
    }
}