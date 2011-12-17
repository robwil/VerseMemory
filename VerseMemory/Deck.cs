using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VerseMemory
{
    [DataContract]
    public class Deck
    {
        [DataMember]
        public List<Slide> remainingSlides { get; set; }

        [DataMember]
        public List<Slide> finishedSlides { get; set; }

        [DataMember]
        public List<Slide> notMemorizedSlides { get; set; }

        public Deck()
        {
            remainingSlides = new List<Slide>();
            finishedSlides = new List<Slide>();
            notMemorizedSlides = new List<Slide>();
        }

        public void SortAll()
        {
            remainingSlides.Sort();
            finishedSlides.Sort();
            notMemorizedSlides.Sort();
        }
    }
}
