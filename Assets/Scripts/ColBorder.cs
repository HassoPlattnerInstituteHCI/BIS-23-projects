using UnityEngine;

namespace DualPantoFramework
{
    public class ColBorder : PantoBoxCollider
    {
        public string text = "";

        public ColBorder()
        {
            isPassable = true;
        }

        public ColBorder(string text) : this()
        {
            this.text = text;
        }

        public override void CreateObstacle()
        {
            UpdateId();
            CreateRail();
        }
        public void CreateObstacle(Vector2 start, Vector2 end, float displacement)
        {
            UpdateId();
            CreateRailForLine(start, end, displacement);
        }

        async void OnTriggerEnter(Collider collider)
        {
            // When target is hit
            //if (collider.tag != "Player") return;
            //PantoManager pantoManager = GameObject.Find("PantoManager").GetComponent<PantoManager>();
            //if (pantoManager != null)
            {

                //if (pantoManager.hasEncounteredColBorder)
                //{
                //pantoManager.Speak(text);
                //}
                //else
                //{
                ////pantoManager.hasEncounteredColBorder = true;
                ////pantoManager.upper.Freeze();
                //await pantoManager.Speak("This is a ColBorder. ColBorders guide you through the level and you can jump them if you press with a little more force against them. On collision, they tell you where they lead to. This one guides " + text);
                //}

            }

        }
    }
}