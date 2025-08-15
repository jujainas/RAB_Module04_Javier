using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Collections.Generic;


namespace RAB_Module04_Challenge_Starter
{
	internal class App : IExternalApplication
	{
		public Result OnStartup(UIControlledApplication app)
        {
            // Create the ribbon tab with try-catch to handle if tab already exists
            try
            {
                app.CreateRibbonTab("Revit Add-in Bootcamp");
            }
            catch (Exception ex)
            {
                // Tab might already exist, continue execution
                Autodesk.Revit.UI.TaskDialog.Show("Tab Creation", $"Note: {ex.Message}");
            }

            // Create the panel
            RibbonPanel panel = app.CreateRibbonPanel("Revit Add-in Bootcamp", "Scavenger Hunt");

            // Get assembly path for creating push buttons
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            // Create the two individual push buttons (Tool 1, Tool 2)
            PushButtonData btnData1 = new PushButtonData("Tool1", "Tool 1", assemblyPath, "RAB_Module04_Challenge_Starter.cmd1");
            PushButtonData btnData2 = new PushButtonData("Tool2", "Tool 2", assemblyPath, "RAB_Module04_Challenge_Starter.cmd2");

            PushButton btn1 = panel.AddItem(btnData1) as PushButton;
            PushButton btn2 = panel.AddItem(btnData2) as PushButton;

            // Create stacked items(Tool 3, Tool 4, Tool 5)
            PushButtonData btnData3 = new PushButtonData("Tool3", "Tool 3", assemblyPath, "RAB_Module04_Challenge_Starter.cmd3");
            PushButtonData btnData4 = new PushButtonData("Tool4", "Tool 4", assemblyPath, "RAB_Module04_Challenge_Starter.cmd4");
            PushButtonData btnData5 = new PushButtonData("Tool5", "Tool 5", assemblyPath, "RAB_Module04_Challenge_Starter.cmd5");

            // Add to a stacked row and explicitly cast each item
            IList<RibbonItem> stackedItems = panel.AddStackedItems(btnData3, btnData4, btnData5);
            PushButton btn3 = stackedItems[0] as PushButton;
            PushButton btn4 = stackedItems[1] as PushButton;
            PushButton btn5 = stackedItems[2] as PushButton;

            // Create split button (Tool 6, Tool 7)
            PushButtonData btnData6 = new PushButtonData("Tool6", "Tool 6", assemblyPath, "RAB_Module04_Challenge_Starter.cmd6");
            PushButtonData btnData7 = new PushButtonData("Tool7", "Tool 7", assemblyPath, "RAB_Module04_Challenge_Starter.cmd7");

            SplitButtonData splitBtnData = new SplitButtonData("SplitButton", "Split Tools");
            SplitButton splitBtn = panel.AddItem(splitBtnData) as SplitButton;
            PushButton btn6 = splitBtn.AddPushButton(btnData6);
            PushButton btn7 = splitBtn.AddPushButton(btnData7);

            // Create pulldown button (More Tools containing Tool 8, Tool 9, Tool 10)
            PulldownButtonData pullDownData = new PulldownButtonData("MoreTools", "More Tools");
            PulldownButton moreToolsBtn = panel.AddItem(pullDownData) as PulldownButton;

            PushButtonData btnData8 = new PushButtonData("Tool8", "Tool 8", assemblyPath, "RAB_Module04_Challenge_Starter.cmd8");
            PushButtonData btnData9 = new PushButtonData("Tool9", "Tool 9", assemblyPath, "RAB_Module04_Challenge_Starter.cmd9");
            PushButtonData btnData10 = new PushButtonData("Tool10", "Tool 10", assemblyPath, "RAB_Module04_Challenge_Starter.cmd10");     

            PushButton btn8 = moreToolsBtn.AddPushButton(btnData8) as PushButton;
            PushButton btn9 = moreToolsBtn.AddPushButton(btnData9) as PushButton;
            PushButton btn10 = moreToolsBtn.AddPushButton(btnData10) as PushButton;

            // Add tooltips and images to buttons
            SetButtonIcons(btn1, btn2, btn3, btn4, btn5, btn6, btn7, moreToolsBtn, btn8, btn9, btn10);

            return Result.Succeeded;
		}

        private void SetButtonIcons(PushButton btn1, PushButton btn2, PushButton btn3, PushButton btn4, PushButton btn5, PushButton btn6,
             PushButton btn7, PulldownButton moreToolsBtn, PushButton btn8, PushButton btn9, PushButton btn10)


        {
            // Set tooltips and icons for buttons
            // Set tooltips and icons for buttons
            btn1.ToolTip = "Executes Tool 1";
            btn1.LargeImage = GetResourceImage("Blue_32.png");
            btn1.Image = GetResourceImage("Blue_16.png");

            btn2.ToolTip = "Executes Tool 2";
            btn2.LargeImage = GetResourceImage("Green_32.png");
            btn2.Image = GetResourceImage("Green_16.png");

            btn3.ToolTip = "Executes Tool 3";
            btn3.Image = GetResourceImage("Red_16.png");

            btn4.ToolTip = "Executes Tool 4";
            btn4.Image = GetResourceImage("Blue_16.png");

            btn5.ToolTip = "Executes Tool 5";
            btn5.Image = GetResourceImage("Green_16.png");

            btn6.ToolTip = "Executes Tool 6";
            btn6.LargeImage = GetResourceImage("Yellow_32.png");
            btn6.Image = GetResourceImage("Yellow_16.png");

            btn7.ToolTip = "Executes Tool 7";
            btn7.LargeImage = GetResourceImage("Red_32.png");
            btn7.Image = GetResourceImage("Red_16.png");

            // Set pulldown button icon
            moreToolsBtn.ToolTip = "Additional Tools";
            moreToolsBtn.LargeImage = GetResourceImage("Blue_32.png");
            moreToolsBtn.Image = GetResourceImage("Blue_16.png");

            // Set icons for pulldown menu items
            btn8.ToolTip = "Executes Tool 8";
            btn8.LargeImage = GetResourceImage("Yellow_32.png");
            btn8.Image = GetResourceImage("Yellow_16.png");

            btn9.ToolTip = "Executes Tool 9";
            btn9.LargeImage = GetResourceImage("Green_32.png");
            btn9.Image = GetResourceImage("Green_16.png");

            btn10.ToolTip = "Executes Tool 10";
            btn10.LargeImage = GetResourceImage("Red_32.png");
            btn10.Image = GetResourceImage("Red_16.png");
        }

        private BitmapImage GetResourceImage(string imageName)
        {
            // Option 1: Load from embedded resources
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = $"{assembly.GetName().Name}.Resources.{imageName}";

            BitmapImage image = new BitmapImage();

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        image.BeginInit();
                        image.StreamSource = stream;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.EndInit();
                        image.Freeze(); // Important for performance
                    }
                }
                return image;
            }
            catch (Exception)
            {
                // Fallback option if resource loading fails
                return null;
            }
        }

        //ESTO ES PARA VER SI SE HA CAMBIADO EN GITHUB
        // Y ESTO ES PARA VER SI SE HA CAMBIADO EN GITHUB OTRA VEZ
		// OTRA PRUEBA MAS


        public Result OnShutdown(UIControlledApplication a)
		{
			return Result.Succeeded;
		}
	}

}
