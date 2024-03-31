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
    static void GroupSort(Team[] Group)
    {
        for (int i = 1; i < Group.Length; i++)
        {
            Team x = Group[i];
            int j = i - 1;
            while (j >= 0 && Group[j].Score < x.Score)
            {
                Group[j + 1] = Group[j];
                j--;
            }
            Group[j + 1] = x;
        }
    }
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

        //sorting groups by teams scores
        GroupSort(MaleGroup);
        GroupSort(FemaleGroup);

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
        GroupSort(FinalGroup);

        //writing final array
        for (int i = 0; i < FinalGroup.Length; i++)
            FinalGroup[i].WriteTeam();
    }
}
