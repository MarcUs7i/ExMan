# ExMan
An exercise manager for programming exercises (Primarly for HTL-Leonding students) <br>
<strong>NOTE: To make this program work, your folder need to have a common bias. </strong> <br>
<strong>The files need to be like this: ..\{Bias}something\File.sln Example: ..\Exercise22\TicTacToe.sln</strong>

# Instructions
1. Place the executable in the main Exercise folder (the root of e.g: "Exercise01")
2. Go to settings and change the variables to your needs.
3. Head back to the Main Menu and it's ready to use.

## Understanding the variables
1. BiasDirectory (The bias for your exercise directories.) Example:     folder: "Exercise01", bias = "Exercise"; folder: "Ex01", bias = "Ex"
2. CleanSolutionBias (The default name of the ps1 script, that cleans the binary folders [not included])
3. DownloadBiasPDF (The bias for the exercise instruction, that you downloaded) Default-Bias: "ue_", Example-PDF: "ue_22_tic_tac_toe.pdf"
4. DownloadBiasPDF (The bias for the exercise starter pack, that you downloaded) Default-Bias: "starter", Example-ZIP: "starter.zip"
5. DefaultSlnEditor (The editor that will be open the solution) Default: "rider"

## Understanding how it works
0. Prepare newest Exercise to export (Gets newest exercise: runs the ps1 file and then archives the exercise in a ZIP file)
1. Prepare selected Exercise to export (the same, but you can select which exercise to export)
2. Clean Solution for newest Exercise (just cleans the newest exercise)
3. Clean Solution for selected Exercise (the same, but here you can select the exercise)
4. Make a zip for newest Exercise (just makes a zip archive of the newest exercise)
5. Make a zip for selected Exercise
6. Extract newest Exercise from Downloads (it creates a new folder, moves the pdf [instruction, not included] to the folder and then extracts the zip content to the folder)
7.  Open newest 'Exercise' (opens the solution with the DefaultSlnEditor and the pdf with msedge [Microsoft Edge])
8.  Display current variables (e.g: BiasDirectory and CleanBuildsBias) [For debugging purposes]
9.  Enter Settings Menu

<br>
<strong>NOTE: The Manager may cause damages to some Exercises, if the Manager isn't used properly.</strong> <br>
<strong>!!! I AM NOT RESPONIBLE FOR ANY DAMAGES, THIS MANAGER WILL CAUSE IF YOU DONT READ THE INSTRUCTIONS !!!</strong>
