[assembly: Rage.Attributes.Plugin("Ped Spawn Example", Author = "TitanSloth", Description = "Messing around.")]
namespace PedSpawn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Rage;

    internal static class EntryPoint
    {
        public static void Main()
        {
            Vector3 spawnPoint;
            Ped Player = Game.LocalPlayer.Character;
            Ped myPed;
            Blip myBlip;

            

            while (true)
            {
                bool dead = false;
                bool player_Has_Weapon = false;
                // Sets the spawnPoint
                spawnPoint = Player.GetOffsetPosition(Vector3.RelativeFront * 10);
                if (Game.IsKeyDown(Keys.F))
                {
                    myPed = new Ped(spawnPoint);

                    myBlip = myPed.AttachBlip();

                    if (!myPed.Exists()) break;
                    if (!myBlip.Exists()) break;

                    // While the player DOES NOT have the weapon...
                    while (!player_Has_Weapon)
                    {
                        // Give the Ped a weapon
                        myPed.Inventory.GiveNewWeapon("WEAPON_ADVANCEDRIFLE", 150, true);
                        // Set this to true
                        player_Has_Weapon = true;

                        if (player_Has_Weapon)
                        {
                            break;
                        }
                    }

                    myPed.RelationshipGroup = new RelationshipGroup("ATTACKER");
                    Game.SetRelationshipBetweenRelationshipGroups("ATTACKER", "PLAYER", Relationship.Hate);

                    myPed.Tasks.FightAgainstClosestHatedTarget(20f);
                }

                while (!dead)
                {
                    if (!myPed.Exists())
                    {
                        if (!myPed.Exists()) myPed.Delete();
                        if (!myBlip.Exists()) myPed.Delete();

                        dead = true;
                    }

                    if (dead)
                    {
                        break;
                    }
                }

                GameFiber.Yield();
            }
        }
    }
}
