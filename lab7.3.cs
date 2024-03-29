using lab73;
using System;
using System.Globalization;
//task #2
namespace lab73
{
    abstract class Team
    {
        protected int _teamID;
        protected int _score;
        public Team(int teamID, int score)
        {
            _teamID = teamID;
            _score = score;
        }

        public int Score => _score;
        public int TeamID => _teamID;

        public virtual void WriteTeam()
        {
            Console.WriteLine($"TeamID: {_teamID} Score: {_score}");
        }
    }

    class MaleTeam : Team
    {
        public MaleTeam(int teamID, int score) : base(teamID, score) { }
        public override void WriteTeam()
        {
            Console.WriteLine($"Male Team, ID: {_teamID} Score: {_score}");
        }
    }

    class FemaleTeam : Team
    {
        public FemaleTeam(int teamID, int score) : base(teamID, score) { }
        public override void WriteTeam()
        {
            Console.WriteLine($"Female Team, ID: {_teamID} Score: {_score}");
        }
    }
}

internal static class Program
{
    static void Main()
    {
        //creating 2 groups
        MaleTeam[] MaleGroup = new MaleTeam[12]
        {
            new MaleTeam(0, 3),
            new MaleTeam(1, 1),
            new MaleTeam(2, 2),
            new MaleTeam(3, 6),
            new MaleTeam(4, 4),
            new MaleTeam(5, 3),
            new MaleTeam(6, 2),
            new MaleTeam(7, 8),
            new MaleTeam(8, 5),
            new MaleTeam(9, 5),
            new MaleTeam(10, 1),
            new MaleTeam(11, 2)
        };

        FemaleTeam[] FemaleGroup = new FemaleTeam[12]
        {
            new FemaleTeam(12, 6),
            new FemaleTeam(13, 6),
            new FemaleTeam(14, 4),
            new FemaleTeam(15, 3),
            new FemaleTeam(16, 1),
            new FemaleTeam(17, 9),
            new FemaleTeam(18, 5),
            new FemaleTeam(19, 2),
            new FemaleTeam(20, 5),
            new FemaleTeam(21, 1),
            new FemaleTeam(22, 3),
            new FemaleTeam(23, 4)
        };

        //sorting groups by teams scores - insertion
        for (int i = 1; i < MaleGroup.Length; i++)
        {
            MaleTeam x = MaleGroup[i];
            int j = i - 1;
            while (j >= 0 && MaleGroup[j].Score < x.Score)
            {
                MaleGroup[j + 1] = MaleGroup[j];
                j--;
            }
            MaleGroup[j + 1] = x;
        }

        for (int i = 1; i < FemaleGroup.Length; i++)
        {
            FemaleTeam x = FemaleGroup[i];
            int j = i - 1;
            while (j >= 0 && FemaleGroup[j].Score < x.Score)
            {
                FemaleGroup[j + 1] = FemaleGroup[j];
                j--;
            }
            FemaleGroup[j + 1] = x;
        }

        //creating sorted array with elements of top-6's
        Team[] FinalGroup = new Team[12];
        int k = 0;
        for (int i = 0; i < 6; i++)
        {
            FinalGroup[k] = MaleGroup[i];
            k++;
        }
        for (int i = 0; i < 6; i++)
        {
            FinalGroup[k] = FemaleGroup[i];
            k++;
        }

        //sorting final array
        for (int i = 1; i < FinalGroup.Length; i++)
        {
            Team x = FinalGroup[i];
            int j = i - 1;
            while (j >= 0 && FinalGroup[j].Score < x.Score)
            {
                FinalGroup[j + 1] = FinalGroup[j];
                j--;
            }
            FinalGroup[j + 1] = x;
        }

        //writing final array
        for (int i = 0; i < FinalGroup.Length; i++)
            FinalGroup[i].WriteTeam();
    }
}
