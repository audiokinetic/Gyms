# Running Tests

Each Gym folder includes a **Resources** sub-folder that contains a TestObject prefab. Add any component needed for testing to it. The default Test script loads the prefab by default.

## Running Unity Tests
There are two ways to run the Gyms Tests: with the UnityAutomation python script or in Unity.

### Testing in Unity
To see available tests, open the Test Runner Window. In Unity, click Window > General > Test Runner.

The window shows all test available. Open the Project folder to view the Gym tests.

![](../../../Documentation/Images/UnityTestRunnerIn.png)

Select the tests you want to run, then right-click > Run Selected Tests.

### Testing with the UnityAutomation Script

Open a command prompt in the Unity folders that contain the Unity project and run the following command:

`python UnityAutomation.py "-f=Path/To/InputFile" "-u=Path/To/Unity/Editor/Unity.exe" "-p=targetPlatform" "-o=Path/To/OutputFile"`

### Arguments

- -f (InputFile): Can be of any format.
Must follow the syntax of an Input File (see below).
- -u (Unity.exe): Must be the same Unity version as the one used to compile the Gyms.
- -p (TargetPlatform): Can be set to Playmode or StandaloneWindows64.
- -o (OutputFile): Can be of any format.
Shows whether a test has passed (True) or failed (False).

This script reads the input file and runs every test named within that file. The test results are written to the output file. For detailed test result information, open the Unity log file called GymsOutput.xml.

>### Optional Arguments
>- `--timeout (-t)`: The maximum duration of the test in seconds. The run will fail without finishing if it lasts longer than the given value. The default value is 600 seconds.

### Input File Formatting (for the UnityAutomation Script)

The input file contains the Gyms and folders you want to run with the automation script. These must be separated by semicolons. There are four types of keyword:

- Example: runs the Gym called "Example".
- \>Example: runs every Gym within the folder "Example" and its sub-folders.
- All: runs every Gyms.
- !Example: skips this specific Gym or folder. Use !>Example to skip every Gym within the folder **Example**.

Here are some examples of keyword usage:

    Runs the BasicAnimNotify test followed by all tests in the 1-Essential folder:
    BasicAnimNotify;>1-Essential;

    Same as above, but skips the EssentialPostEvent test contained in 1-Essential:
    BasicAnimNotify;>1-Essential;!EssentialPostEvent;

    Runs all the test, but skips anything contained in the 9-Testing folder:    
    All;!>9-Testing



The following examples demonstrate incorrect keyword usage. Avoid using keywords like this:

    Runs all the test, but won't run the tests contained in 1-Essential twice. 
    In other words, the (>1-Essential;) keyword is usless:   
    All;>1-Essential;

    Runs all the text contained in the 1-Essential folder but still skips EssentialPostEvent:
    >1-Essential;!EssentialPostEvent;EssentialPostEvent;



>### Notes
>- An empty input file runs all of the tests.
>- An input file containing only keywords to skip (!Example) is not supported.

## Testing Tips

The test starts by loading the Gym and adding the TestObject prefab to it.

Tests have multiple tools available.

### Assert Equal

In some tests, you might expect certain values to appear. Use the `Assert.AreEqual` function to check for different types of values.

### Are Approximately Equal

Use  `AreApproximatelyEqual` to check for `float` with a specified precision.

### Expected Log Error
You can create test scenarios that throw errors or warnings. The `ExpectedLogError` function helps to test these cases. This function must be called before the warning/error is thrown. `pattern` is a part of the error that is expected to be thrown and `occurrences` is the number of times the error is expected to appear.

### Post Silence
`PostSilence` will posts a silent event and returns the playing ID. Use it to compare playing IDs.