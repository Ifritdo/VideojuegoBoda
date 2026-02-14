[System.Serializable]
public class ScoreData
{
    public string name;
    public int points;

    public ScoreData(string n, int p)
    {
        name = n;
        points = p;
    }
}
