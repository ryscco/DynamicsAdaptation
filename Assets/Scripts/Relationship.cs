public class Relationship
{
    public NPC npc1, npc2;
    public float relationshipValue;
    public Relationship(NPC npc1, NPC npc2, float relVal)
    {
        this.Relate(npc1, npc2, relVal);
    }
    public void Relate(NPC npc1, NPC npc2, float relVal)
    {
        this.npc1 = npc1;
        this.npc2 = npc2;
        this.relationshipValue = relVal;
    }
}