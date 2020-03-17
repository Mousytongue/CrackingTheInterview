using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackingTheInterview
{
    /// <summary>
    /// Object oriented design principales
    /// 
    /// Step 1 - Handle Ambiguity
    /// What you trying to program can be very vauge - Try to narrow down the scope of purpose and audience.
    /// Who, what , where, when, how and why.
    /// 
    /// Step 2 - Define Core Objects
    /// Core objects will likely be established as classes, Figure out all the needed components
    /// 
    /// Step 3 - Analyze relationships
    /// With the core objects established. How are they related to each other, Inheritance will requuire this relation to be established.
    /// One-to-one, One-to-many? Inherits? Membership?
    /// 
    /// Step 4 - Investigate Actions
    /// What will the core objects do? Will they have a function or are they simply data?
    /// 
    /// Design Pattersn
    /// There are many many different design patterns. There is no "right" pattern to study/use. Fit your design around the problem. 
    /// Use what works for the situation
    ///
    /// 
    ///
    /// </summary>

    // Singleton Class
    // A singleton is a class that ensures that there is only 1 instance of that class. Very useful for when there is a need for a "global"
    // object. Event managers are something of that nature.
    public class Restaurant
    {
        private static Restaurant _instance = null;
        //protected class. Can only be accessed within this class, and by derived class instances.
        //Means that you cannot create this class outside of the getInstance() method
        //Which means that whenever someone wants this class, its gaurenteed to only return the first instance of this class
        protected Restaurant() { }

        public static Restaurant getInstance()
        {
            if (_instance == null)
                _instance = new Restaurant();
            return _instance;
        }
    }

    // Factory Method
    // This offers an interface for creating instances of this class. Each subclass decides which class to instantiate.
    // You may want to setup the creator class as abstance, not providing a implementation for the Factory method.
    // Or it may be a concrete method that provides the implementation - Using different parameters upon creation is how 
    // the creator would make different types of subclasses
    public enum GameType
    {
        Poker,
        BlackJack
    }
    public class CardGame
    {
        public static CardGame createCardGame(GameType type)
        {
            if (type == GameType.Poker)
            {
               // return new PokerGame();
            }
            else if (type == GameType.BlackJack)
            {
               // return new BlackJackGame();
            }
            return null;
        }
    }

    // 7.1 Card Game
    //Design the data structures for a generic deck of cards.Explain how you would subclass the data structures to implement blackjack.
    #region GenericPlayingCards
    //abstract classes cannot be instantiated. They provide an interface for class creation
    public enum Suit { Club, Diamond, Heart, Spade }
    public abstract class Card
    {
        private bool available = true;

        //Value of card - 11 is Jack - 12 is Queen - 13 is King - 1 is Ace
        protected int faceValue;
        protected Suit mSuit;

        public Card(int c, Suit s)
        {
            faceValue = c;
            mSuit = s;
        }

        public abstract int value();
        public Suit GetSuit() { return mSuit; }

        public bool IisAvailable() { return available; }
        public void MarkUnavailable() { available = false; }
        public void MarkAvailable() { available = true; }
    }

    //Creates a generic Deck type, but restricts that type to Card
    public class Deck<T> where T : Card
    {
        private List<T> cards;
        private int dealtIndex = 0;
        public void SetDeckOfCards(List<T> deckOfCards) { cards = deckOfCards; }
        public void Shuffle() { }
        public int RemainingCards() { return cards.Count - dealtIndex; }
        public T dealCard() {
            if (cards == null) return null;
            T card = cards[dealtIndex];
            dealtIndex++;
            return card;
        }
    }
    
    public class Hand<T> where T : Card
    {
        protected List<T> cards = new List<T>();

        public int score()
        {
            int score = 0;
            foreach (T card in cards)
            {
                score += card.value();
            }
            return score;
        }

        public void addCard(T card)
        {
            cards.Add(card);
        }
    }
    #endregion
    #region BlackJack Gametype
    public class BlackJackCard : Card
    {
        public BlackJackCard(int c, Suit s) : base(c, s) { faceValue = c; mSuit = s; }

        //Since BlackJackCard inherits from Card, override needs to be used on value() to tell the compiler
        //To use this method instead of the base class method. In this case, the base class is an abstract type
        //So without the override, it wouldnt compile at all since no valid implmentation of value() was created
        override public int value()
        {
            if (isAce()) return 1;
            else if (faceValue >= 11 && faceValue <= 13) return 10;
            else return faceValue;
        }

        public int minValue()
        {
            if (isAce()) return 1;
            else return value();
        }

        public int maxValue()
        {
            if (isAce()) return 11;
            else return value();
        }

        public bool isAce() { return faceValue == 1; }
        public bool isFaceCard() { return faceValue >= 11 && faceValue <= 13; }
    }
    #endregion

    // 7.2 Call Center
    //Imagine you have a call center with three levels of employees: respondent, manager, and director. 
    //An incoming telephone call must be first allocated to a respondent who is free. 
    //If the respondent can't handle the call, he or she must escalate the call to a manager. 
    //If the manager is not free or not able to handle it, then the call should be escalated to a director. 
    //Design the classes and data structures for this problem. 
    //Implement a method dispatchCall () which assigns a call to the first available employee.
    #region Employee
    public enum JobTitle { Respondent, Manager, Director }
    public abstract class Employee
    {
        //Things in common - Address - name - Job title - Age
        protected string mAddress;
        protected string mName;
        protected JobTitle mTitle;
        protected int mAge;

        public Employee(string address, string name, JobTitle title, int age)
        {
            mAddress = address;
            mName = name;
            mTitle = title;
            mAge = age;
        }

        protected bool isAvailable = true;

        public bool Available() { return isAvailable; }
        public string GetAddress() { return mAddress; }
        public JobTitle GetTitle() { return mTitle; }
        public int GetAge() { return mAge; }
        public string GetName() { return mName; }
    }

    public class Respondent : Employee
    {
        public Respondent(string address, string name, JobTitle jobtitle, int age) : base(address, name, jobtitle, age)
        {
            mAddress = address;
            mName = name;
            mTitle = jobtitle;
            mAge = age;
        }

        public bool RespondentJobFunction()
        {
            return true;
        }
    }

    public class Manager : Employee
    {
        public Manager(string address, string name, JobTitle jobtitle, int age) : base(address, name, jobtitle, age)
        {
            mAddress = address;
            mName = name;
            mTitle = jobtitle;
            mAge = age;
        }

        public bool ManagerJobFunction()
        {
            return true;
        }
    }

    public class Director : Employee
    {
        public Director(string address, string name, JobTitle jobtitle, int age) : base(address, name, jobtitle, age)
        {
            mAddress = address;
            mName = name;
            mTitle = jobtitle;
            mAge = age;
        }

        public bool DirectorJobFunction()
        {
            return true;
        }
    }

    public enum IssueType
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        Payment
    }
    public class CallCenter
    {
        List<Director> Directors = new List<Director>();
        List<Manager> Managers = new List<Manager>();
        List<Respondent> Respondents = new List<Respondent>();
        LinkedList<Call> CallList = new LinkedList<Call>();
        private bool IsProcessingCalls = false;
        //Call center is going to get a queue of calls

        public CallCenter() { PopulateEmployees(); }
        private void PopulateEmployees()
        {
            Director BigD = new Director("BigDAddress", "Mr D.", JobTitle.Director, 54);
            Directors.Add(BigD);
            Manager Man1 = new Manager("Man1Address", "Man 1", JobTitle.Manager, 35);
            Managers.Add(Man1);
            Manager Man2 = new Manager("Man2Address", "Man 2", JobTitle.Manager, 44);
            Managers.Add(Man2);
            Manager Man3 = new Manager("Man3Address", "Man 3", JobTitle.Manager, 31);
            Managers.Add(Man3);
            Respondent Res1 = new Respondent("Res1Adress", "Res 1", JobTitle.Respondent, 18);
            Respondents.Add(Res1);
            Respondent Res2 = new Respondent("Res2Adress", "Res 2", JobTitle.Respondent, 19);
            Respondents.Add(Res2);
            Respondent Res3 = new Respondent("Res3Adress", "Res 3", JobTitle.Respondent, 20);
            Respondents.Add(Res3);
            Respondent Res4 = new Respondent("Res4Adress", "Res 4", JobTitle.Respondent, 21);
            Respondents.Add(Res4);
            Respondent Res5 = new Respondent("Res5Adress", "Res 5", JobTitle.Respondent, 22);
            Respondents.Add(Res5);
        }

        public void AcceptCall(IssueType issue)
        {
            Call call = new Call(issue);
            CallList.AddLast(call);
            if (!IsProcessingCalls)
                StartProcessingCalls();
        }

        public void EscalateCall(Call call)
        {
            CallList.AddFirst(call);
        }

        private void StartProcessingCalls()
        {
            if (IsProcessingCalls)
                return;

            IsProcessingCalls = true;
            while (CallList.Count != 0)
            {
                Call currCall = CallList.First();

                switch (currCall.GetIssue())
                {
                    case IssueType.LevelOne:
                        break;

                    case IssueType.LevelTwo:
                        break;

                    case IssueType.LevelThree:
                        break;

                    case IssueType.Payment:
                        break;
                }

            }
            IsProcessingCalls = false;
        }
    }
    public class Call
    {
        private IssueType mIssue;

        public Call(IssueType issue)
        {
            mIssue = issue;
        }

        public IssueType GetIssue() { return mIssue; }
    }

    #endregion
}
