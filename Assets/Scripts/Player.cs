public class Player
{
    public string name;
    public float rage;
    public float charisma;
    public float luck;
    public float talent;
    public float knowledgeCarry;
    public float knowledgeGanker;
    public float knowledgeSupport;

    public Player(string name, float rage, float charisma, float luck, float talent, float knowledgeCarry, float knowledgeGanker, float knowledgeSupport)
    {
        this.name = name;
        this.rage = rage;
        this.charisma = charisma;
        this.luck = luck;
        this.talent = talent;
        this.knowledgeCarry = knowledgeCarry;
        this.knowledgeGanker = knowledgeGanker;
        this.knowledgeSupport = knowledgeSupport;
    }
}