using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weasel_Controller
{
    class KukaRoboter
    {
        //RoboDK Variables
        private RoboDK RDK = null;
        private RoboDK.Item ROBOT = null;
        private const bool MOVE_BLOCKING = false;
        //string objekt = "";
        private static double[] jointsNEU;

        //Constructor
        public KukaRoboter()
        {
            //Gets the Robot
            if (!Check_RDK())
            {
                RDK = new RoboDK();

                // Check if RoboDK started properly
                if (Check_RDK())
                {
                    SelectRobot();
                }

                // set default movement on the simulator only:
                SwitchSimulationMode();

                // display RoboDK by default:
                // Not working: rad_RoboDK_show.PerformClick();

                // Set incremental moves in cartesian space with respect to the robot reference frame
                // Only buttons: rad_Move_wrt_Reference.PerformClick();

                //numStep.Value = 10; // set movement steps of 50 mm or 50 deg by default
            }
        }

        //Methods
        public bool Check_RDK()
        {
            // check if the RDK object has been initialised:
            if (RDK == null)
            {
                return false;
            }

            // Check if the RDK API is connected
            if (!RDK.Connected())
            {
                // Attempt to connect to the RDK API
                if (!RDK.Connect())
                {
                    return false;
                }
            }
            return true;
        }

        public bool Check_ROBOT(bool ignore_busy_status = false)
        {
            if (!Check_RDK())
            {
                return false;
            }
            if (ROBOT == null || !ROBOT.Valid())
            {
                return false;
            }
            try
            {
            }
            catch (RoboDK.RDKException rdkex)
            {
                Console.WriteLine(rdkex.ToString());
                return false;
            }

            // Safe check: If we are doing non blocking movements, we can check if the robot is doing other movements with the Busy command
            if (!MOVE_BLOCKING && (!ignore_busy_status && ROBOT.Busy()))
            {
                return false;
            }
            return true;
        }

        public void SelectRobot()
        {
            if (!Check_RDK())
            {
                ROBOT = null;
                return;
            }
            ROBOT = RDK.ItemUserPick("Select a robot", RoboDK.ITEM_TYPE_ROBOT);
            if (ROBOT.Valid())
            {
                ROBOT.NewLink();               
            }
        }

        public void SwitchSimulationMode()
        {
            // Check that there is a link with RoboDK
            if (!Check_ROBOT()) { return; }

            // Important: stop any previous program generation (if we selected offline programming mode)
            RDK.Finish();

            // Set to simulation mode
            RDK.setRunMode(RoboDK.RUNMODE_SIMULATE);
        }

        public void SwitchRealMode()
        {
            if (!Check_ROBOT()) { return; }

            // Important: stop any previous program generation (if we selected offline programming mode)
            RDK.Finish();

            // Connect to real robot
            if (ROBOT.Connect())
            {
                // Set to Run on Robot robot mode:
                RDK.setRunMode(RoboDK.RUNMODE_RUN_ROBOT);
            }
            else
            {
                Console.WriteLine("Can't connect to the robot. Check connection and parameters.");
                SwitchSimulationMode();
            }
        }

        public void Move(string postions)
        {
            if (!Check_ROBOT()) { Console.WriteLine("Keine Verbindung zum Roboter"); return; }

            jointsNEU = new double[6];
            jointsNEU = String_2_Values_NEU(postions);

            // make sure RDK is running and we have a valid input
            if (!Check_ROBOT() || jointsNEU == null) { return; }

            try
            {
                ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
                ROBOT.WaitMove();
            }
            catch (RoboDK.RDKException rdkex)
            {
                Console.WriteLine("Problems moving the robot: " + rdkex.Message);
            }
        }

        public double[] String_2_Values_NEU(string strvalues)
        {
            double[] dvalues = null;
            try
            {
                dvalues = Array.ConvertAll(strvalues.Split(';'), Double.Parse);
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Invalid input: " + strvalues);
            }
            return dvalues;
        }

        public void MoveToAnimation()
        {
            jointsNEU = new double[6];

            // Home
            jointsNEU = ROBOT.JointsHome();
            ROBOT.MoveJ(jointsNEU);
            ROBOT.WaitMove();
            // Step 1
            jointsNEU = String_2_Values_NEU("84,9 ; -57,14 ; 78,47 ; -18,18 ; 43,62 ; -8,54");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            // Step 2
            jointsNEU = String_2_Values_NEU("83,53 ; -29,04 ; 78,49 ; -41,31 ; 18,07 ; 16,71");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            // Step 3
            jointsNEU = String_2_Values_NEU("85,15 ; -32,34 ; 89,08 ; -62,02 ; 14,23 ; 39,59");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            // Step 4
            jointsNEU = String_2_Values_NEU("85,32 ; -30,44 ; 84,34 ; -53,15 ; 15,84 ; 30,54");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            // Step 5
            jointsNEU = String_2_Values_NEU("85,32 ; -51,53 ; 81,34 ; -21,95 ; 35,77 ; -3,43");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            // Step 6
            jointsNEU = String_2_Values_NEU("87,48 ; -50,58 ; 96,41 ; 4,33 ; 44,71 ; 70,63");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            //GreiferAuf();

            // Step 7
            jointsNEU = String_2_Values_NEU("88,15 ; -44 ; 99,12 ; -0,67 ; 35,19 ; 75,15");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 8
            jointsNEU = String_2_Values_NEU("88,22 ; -33,98 ; 97,68 ; -0,86 ; 26,61 ; 75,7");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 9
            jointsNEU = String_2_Values_NEU("87,8 ; -63,68 ; 93,39 ; 0,8 ; 59,55 ; 73,3");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 10
            jointsNEU = String_2_Values_NEU("112,01 ; -31,01 ; 54,46 ; 0,35 ; 66,08 ; 97,77");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 11
            jointsNEU = String_2_Values_NEU("112,90 ; -17,38 ; 37,06 ; 0,33 ; 71,35 ; 98,69");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 12
            jointsNEU = String_2_Values_NEU("112,93 ; -11,77 ; 36,77 ; 0,34 ; 66,04 ; 98,68");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 13
            jointsNEU = String_2_Values_NEU("112,88 ; -20,02 ; 35,46 ; 0,32 ; 75,59 ; 98,70");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();
            //Step 14 (Step 9)
            jointsNEU = String_2_Values_NEU("87,8 ; -63,68 ; 93,39 ; 0,8 ; 59,55 ; 73,3");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Home
            jointsNEU = ROBOT.JointsHome();
            ROBOT.MoveJ(jointsNEU);
            ROBOT.WaitMove();
        }

        public void AllMovements()
        {
            // Home
            jointsNEU = ROBOT.JointsHome();
            ROBOT.MoveJ(jointsNEU);
            ROBOT.WaitMove();

            // Step 1
            jointsNEU = String_2_Values_NEU("84,9 ; -57,14 ; 78,47 ; -18,18 ; 43,62 ; -8,54");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 2
            jointsNEU = String_2_Values_NEU("83,53 ; -29,04 ; 78,49 ; -41,31 ; 18,07 ; 16,71");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 3
            jointsNEU = String_2_Values_NEU("85,15 ; -32,34 ; 89,08 ; -62,02 ; 14,23 ; 39,59");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 4
            jointsNEU = String_2_Values_NEU("85,32 ; -30,44 ; 84,34 ; -53,15 ; 15,84 ; 30,54");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 5
            jointsNEU = String_2_Values_NEU("85,32 ; -51,53 ; 81,34 ; -21,95 ; 35,77 ; -3,43");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 6
            jointsNEU = String_2_Values_NEU("87,48 ; -50,58 ; 96,41 ; 4,33 ; 44,71 ; 70,63");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 7
            jointsNEU = String_2_Values_NEU("88,15 ; -44 ; 99,12 ; -0,67 ; 35,19 ; 75,15");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 8
            jointsNEU = String_2_Values_NEU("88,22 ; -33,98 ; 97,68 ; -0,86 ; 26,61 ; 75,7");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 9
            jointsNEU = String_2_Values_NEU("87,8 ; -63,68 ; 93,39 ; 0,8 ; 59,55 ; 73,3");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 10
            jointsNEU = String_2_Values_NEU("30,77 ; -69,06 ; 101,41 ; 1,17 ; 57,84 ; 16,04");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 11
            jointsNEU = String_2_Values_NEU("17,07 ; -16,59 ; 36,76 ; 0,58 ; 68,98 ; 83,75");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 12
            jointsNEU = String_2_Values_NEU("17,07 ; -11 ; 35,6 ; 0,6 ; 64,56 ; 83,7");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Step 13
            jointsNEU = String_2_Values_NEU("17,07 ; -20,82 ; 34,78 ; 0,56 ; 75,2 ; 83,81");
            ROBOT.MoveJ(jointsNEU, MOVE_BLOCKING);
            ROBOT.WaitMove();

            // Home
            jointsNEU = ROBOT.JointsHome();
            ROBOT.MoveJ(jointsNEU);
            ROBOT.WaitMove();
        }
    }
}
