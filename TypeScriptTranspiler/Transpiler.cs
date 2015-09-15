
// http://stackoverflow.com/questions/17243897/typescript-via-tsc-command-output-to-single-file-without-concatenation
// tsc -out output.js filea.ts fileb.ts... <- output to single file output.js
// tsc -out output filea.ts fileb.ts... <- output individual files to dir output
// tsc -out output/output.js filea.ts fileb.ts... <- output to single file in another directory
// https://blog.appdynamics.com/devops/8-steps-migrating-javascript-typescript/
public partial class TranspilerWrapper
{


    // msiexec /a D:\Temp\AjaxMinSetup.msi /qb TARGETDIR=D:\Temp\AjaxMinSetup\
    // http://nickberardi.com/yahoo-yui-compressor-vs-microsoft-ajax-minifier-vs-google-closure-compiler/
    // https://visualstudiogallery.msdn.microsoft.com/b1fff87e-d68b-4266-8bba-46fad76bbf22
    // http://www.microsoft.com/en-us/download/details.aspx?id=48593
    public static string MinifyCss(string[] filez)
    {
        string ret = null;
        System.Text.StringBuilder content = new System.Text.StringBuilder();

        Microsoft.Ajax.Utilities.Minifier mifi = new Microsoft.Ajax.Utilities.Minifier();
        Microsoft.Ajax.Utilities.CodeSettings cs = new Microsoft.Ajax.Utilities.CodeSettings();
        Microsoft.Ajax.Utilities.CssSettings css = new Microsoft.Ajax.Utilities.CssSettings();
        css.ColorNames = Microsoft.Ajax.Utilities.CssColor.Hex;
        css.CommentMode = Microsoft.Ajax.Utilities.CssComment.None;
        css.BlocksStartOnSameLine = Microsoft.Ajax.Utilities.BlockStart.SameLine;
        css.CssType = Microsoft.Ajax.Utilities.CssType.FullStyleSheet;
        css.IndentSize = 0;
        css.MinifyExpressions = true;
        // css.LineTerminator ="\n";
        css.OutputMode = Microsoft.Ajax.Utilities.OutputMode.SingleLine;
        css.TermSemicolons = false;


        // cs.AddRenamePair("delete", "fooDelete");
        cs.MinifyCode = true;
        cs.OutputMode = Microsoft.Ajax.Utilities.OutputMode.SingleLine;
        cs.CollapseToLiteral = true;
        cs.EvalTreatment = Microsoft.Ajax.Utilities.EvalTreatment.MakeAllSafe;
        cs.IndentSize = 0;
        cs.InlineSafeStrings = true;
        cs.LocalRenaming = Microsoft.Ajax.Utilities.LocalRenaming.CrunchAll;
        cs.MacSafariQuirks = true;
        cs.PreserveFunctionNames = true;
        cs.RemoveFunctionExpressionNames = true;
        cs.RemoveUnneededCode = true;
        cs.StripDebugStatements = true;
        cs.PreserveImportantComments = false;

        foreach (string file in filez)
        {
            content.Append(mifi.MinifyStyleSheet(System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8), css, cs));
            content.Append(";");
        }
        ret = content.ToString();
        content.Length = 0;
        content = null;

        return ret;
    }


    // http://danielwertheim.se/2012/11/16/customizing-the-minification-of-javascript-in-asp-net-mvc4-allowing-reserved-words/
    public static string MinifyJavaScript(string[] filez)
    {
        string ret = null;
        System.Text.StringBuilder content = new System.Text.StringBuilder();

        Microsoft.Ajax.Utilities.Minifier mifi = new Microsoft.Ajax.Utilities.Minifier();
        Microsoft.Ajax.Utilities.CodeSettings cs = new Microsoft.Ajax.Utilities.CodeSettings();
        
        // cs.AddRenamePair("delete", "fooDelete");
        cs.MinifyCode = true;
        cs.OutputMode = Microsoft.Ajax.Utilities.OutputMode.SingleLine;
        cs.CollapseToLiteral = true;
        cs.EvalTreatment = Microsoft.Ajax.Utilities.EvalTreatment.MakeAllSafe;
        cs.IndentSize = 0;
        cs.InlineSafeStrings = true;
        cs.LocalRenaming = Microsoft.Ajax.Utilities.LocalRenaming.CrunchAll;
        cs.MacSafariQuirks = true;
        cs.PreserveFunctionNames = true;
        cs.RemoveFunctionExpressionNames = true;
        cs.RemoveUnneededCode = true;
        cs.StripDebugStatements = true;
        cs.PreserveImportantComments = false;

        foreach (string file in filez)
        {
            content.Append(mifi.MinifyJavaScript(System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8), cs));
            content.Append(";");
        }
        ret = content.ToString();
        content.Length = 0;
        content = null;

        return ret;
    }



    private static System.Diagnostics.Process StartHiddenProcess(string filename, string arguments)
    {

        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
        psi.FileName = filename;
        psi.Arguments = arguments;
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;

        return System.Diagnostics.Process.Start(psi);
    } // End Function StartHiddenProcess 


    // apt-get install nodejs npm
    // npm install -g typescript
    private static void TranspileUnix(string inFile, string outFile)
    {
        // inFile = "/root/sources/go/hw.ts";
        string exeName = @"/usr/bin/node";
        string tsc = @"/usr/local/lib/node_modules/typescript/bin/tsc";
        
        if (!System.IO.File.Exists("/usr/bin/node") && System.IO.File.Exists("/usr/bin/nodejs"))
        {
            // ln -sf source target
            // ln -s /usr/bin/nodejs /usr/bin/node
            using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start("ln", "-s /usr/bin/nodejs /usr/bin/node"))
            {
                proc.WaitForExit();
            }
        }


        if (!System.IO.File.Exists(exeName) || !System.IO.File.Exists(tsc))
            throw new TranspilerConfigurationException("NodeJS misconfigured...");


        string arguments = string.Format(@"node ""{0}"" ""{1}""",
            tsc, inFile
        );

        // nodejs /usr/local/lib/node_modules/typescript/bin/tsc "filename.ts"

        //using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(filename, arguments))
        using (System.Diagnostics.Process proc = StartHiddenProcess(exeName, arguments))
        {
            proc.WaitForExit();
        }

    } // End Sub TranspileUnix 


    // http://sourceforge.net/projects/nodejsportable/?source=typ_redirect
    // "%~dp0\node.exe"  "%~dp0\node_modules\typescript\bin\tsc" %*
    private static void TranspileWindows(string inFile, string outFile)
    {
        // inFile = @"d:\stefan.steiger\documents\visual studio 2013\Projects\TypeScriptTranspiler\TypeScriptTranspiler\Test.ts";
        // outFile = @"D:\output.js";
        
        string nodejsPath = @"D:\Programme\NodeJSPortable";
        string exeName = System.IO.Path.Combine(nodejsPath, @"App\NodeJS\node.exe");
        string tsc = System.IO.Path.Combine(nodejsPath, @"Data\node_modules\typescript\bin\tsc");

        if (!System.IO.File.Exists(exeName) || !System.IO.File.Exists(tsc))
            throw new TranspilerConfigurationException("NodeJS misconfigured...");

        string arguments = string.Format(@"""{0}"" ""{1}"" {2}"
            , tsc, inFile, outFile == null ? "" : "-out \"" + outFile + "\"");

        // "D:\Programme\NodeJSPortable\App\NodeJS\node.exe" "D:\Programme\NodeJSPortable\Data\node_modules\typescript\bin\tsc" "D:\Programme\NodeJSPortable\Data\myscript.ts" -out "D:\output.js"

        //using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(filename, arguments))
        using (System.Diagnostics.Process proc = StartHiddenProcess(exeName, arguments))
        {
            proc.WaitForExit();
        }
    } // End Sub TranspileWindows 


    public static void Transpile(string inFile, string outFile)
    {
        if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
        {
            TranspileUnix(inFile, outFile);
            return;
        }

        TranspileWindows(inFile, outFile);
    } // End Sub Transpile


    public static void Transpile()
    {
        string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        dir = System.IO.Path.Combine(dir, "..");
        dir = System.IO.Path.Combine(dir, "..");

        dir = System.IO.Path.GetFullPath(dir);

        string inFile = System.IO.Path.Combine(dir, "Test.ts");
        string outFile = System.IO.Path.Combine(dir, "Test.js");

        Transpile(inFile, outFile);
    } // End Sub Transpile


} // End Class TranspilerWrapper 
