namespace CoreMvcTemplate.Models.Statistics
{
    public class MainModel
    {
        public MainModel()
        {
            ActivePassiveBlogKeys = new List<string>();
            ActivePassiveBlogValues = new List<int>();

            BlogCorpTypeKeys = new List<string>();
            BlogCorpTypeValues = new List<int>();

            OtherPageInfoKeys = new List<string>();
            OtherPageInfoValues = new List<int>();
        }

        public List<string> ActivePassiveBlogKeys { get; set; }
        public List<int> ActivePassiveBlogValues { get; set; }


        public List<string> BlogCorpTypeKeys { get; set; }
        public List<int> BlogCorpTypeValues { get; set; }

        public List<string> OtherPageInfoKeys { get; set; }
        public List<int> OtherPageInfoValues { get; set; }



    }
}
