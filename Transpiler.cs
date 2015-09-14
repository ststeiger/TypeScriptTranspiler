
// http://stackoverflow.com/questions/17243897/typescript-via-tsc-command-output-to-single-file-without-concatenation
// tsc -out output.js filea.ts fileb.ts... <- output to single file output.js
// tsc -out output filea.ts fileb.ts... <- output individual files to dir output
// tsc -out output/output.js filea.ts fileb.ts... <- output to single file in another directory
public class TranspilerWrapper
{


    public TranspilerWrapper()
	{
	}


    // apt-get install nodejs npm
    // npm install -g typescript
    public static void TranspileUnix()
    {
        if (!System.IO.File.Exists("/usr/bin/node") && System.IO.File.Exists("/usr/bin/nodejs"))
        {
            // ln -s /usr/bin/nodejs /usr/bin/node
        }

    }



    // http://sourceforge.net/projects/nodejsportable/?source=typ_redirect
    // "%~dp0\node.exe"  "%~dp0\node_modules\typescript\bin\tsc" %*
    // "D:\Programme\NodeJSPortable\App\NodeJS\node.exe" "D:\Programme\NodeJSPortable\Data\node_modules\typescript\bin\tsc" "D:\Programme\NodeJSPortable\Data\myscript.ts" -out "D:\output.js"
    public static void TranspileWindows()
    { 
    
    }


    public static void Transpile()
    {
        if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
        { 
        
        }
    }


}
