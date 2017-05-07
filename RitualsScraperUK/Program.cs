using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RitualsScraperUK
{
    class Program
    {
        //UK URLs
        //public const string body = "https://www.rituals.com/en-gb/body";
        //public const string atHome = "https://www.rituals.com/en-gb/at-home";
        //public const string makeUp = "https://www.rituals.com/en-gb/make-up";
        //public const string gifts = "https://www.rituals.com/en-gb/gifts";
        //public const string face = "https://www.rituals.com/en-gb/face";
        //public const string perfume = "https://www.rituals.com/en-gb/perfume";
        //public const string clothing = "https://www.rituals.com/en-gb/clothing";
        //public const string babyAndMom = "https://www.rituals.com/en-gb/baby-and-mom";
        //public const string travelEssentials = "https://www.rituals.com/en-gb/travel-essentials";
        //public const string men = "https://www.rituals.com/en-gb/men";
        //public const string collections = "https://www.rituals.com/en-gb/collections";

        //US URLs
        public const string body = "https://www.rituals.com/en-us/body";
        public const string atHome = "https://www.rituals.com/en-us/at-home";
        public const string face = "https://www.rituals.com/en-us/face";
        public const string makeUp = "https://www.rituals.com/en-us/make-up";
        public const string gifts = "https://www.rituals.com/en-us/gifts";
        public const string perfume = "https://www.rituals.com/en-us/perfume";
        public const string babyAndMom = "https://www.rituals.com/en-us/baby-mama";
        public const string travelEssentials = "https://www.rituals.com/en-us/travel";
        public const string forHim = "https://www.rituals.com/en-us/for-him";
        public const string collections = "https://www.rituals.com/en-us/collections";
        static void Main(string[] args)
        {
            Scraper scraper3 = new Scraper(body, "Body");
            Scraper scraper8 = new Scraper(atHome, "At Home");
            Scraper scraper = new Scraper(gifts, "Gifts");
            Scraper scraper1 = new Scraper(face, "Face");
            Scraper scraper9 = new Scraper(makeUp, "Make Up");
            Scraper scraper2 = new Scraper(perfume, "Perfume");
            Scraper scraper4 = new Scraper(babyAndMom, "Baby & Mama");
            Scraper scraper5 = new Scraper(travelEssentials, "Travel");
            Scraper scraper6 = new Scraper(forHim, "For Him");
            Scraper scraper7 = new Scraper(collections, "Collections");

            Thread thread3 = new Thread(new ThreadStart(scraper3.NavigateToCategoryPage));
            Thread thread8 = new Thread(new ThreadStart(scraper8.NavigateToCategoryPage));
            Thread thread = new Thread(new ThreadStart(scraper.NavigateToCategoryPage));
            Thread thread1 = new Thread(new ThreadStart(scraper1.NavigateToCategoryPage));
            Thread thread9 = new Thread(new ThreadStart(scraper9.NavigateToCategoryPage));
            Thread thread2 = new Thread(new ThreadStart(scraper2.NavigateToCategoryPage));
            Thread thread4 = new Thread(new ThreadStart(scraper4.NavigateToCategoryPage));
            Thread thread5 = new Thread(new ThreadStart(scraper5.NavigateToCategoryPage));
            Thread thread6 = new Thread(new ThreadStart(scraper6.NavigateToCategoryPage));
            Thread thread7 = new Thread(new ThreadStart(scraper7.NavigateToCategoryPage));

            //thread.Start();
            //thread1.Start();
            //thread2.Start();
            //thread3.Start();
            //thread8.Start();
            //thread9.Start();
            //thread4.Start();
            //thread5.Start();
            thread6.Start();
            thread7.Start();
        }
    }
}
