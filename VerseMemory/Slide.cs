using System;
using System.Runtime.Serialization;

namespace VerseMemory
{
    [DataContract]
    class Slide : IComparable
    {
        [DataMember]
        public String question { get; set; }
        
        [DataMember]
        public String answer { get; set; }
        
        [DataMember]
        public int weight { get; set; }
        
        [DataMember]
        public bool isMemorized { get; set; }
    
        public Slide(String question, String answer)
        {
            this.question = question;
            this.answer = answer;
        }

        /**
         * Sorts in descending order (higher elements first).
         **/
        public int CompareTo(object obj)
        {
            Slide otherSlide = obj as Slide;
            if (otherSlide != null)
                return otherSlide.weight - weight;

            return 0;
        }
    }
}
