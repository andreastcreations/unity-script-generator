# Unity_ScriptGenerator
 
As the name suggests, this is a Script Generator.
 
You can create a scriptable object that holds all your script data, press "generate" and voila!
You have your script and you also have a script template asset that you can save and reuse whenever you want.
 
The Generator takes some C# and Unity restrictions into account.
For example, when you want to add a static class, you cannot inherit from MonoBehaviour and all your methods become static.
 
1. Right click in the project folder -> Create -> C# Script Template

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/1.%20Menu.jpg)

2. New C# Template asset (Scriptable Object)

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/2.%20Empty%20Template.jpg)

3. Add the script's declaration.
Choose between class/scriptable object, interface, struct or enum. Add XML summary if you want.
Add variables and properties or any custom code you may need before adding your methods.

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/3.%20Add%20Declaration%20and%20Variables.jpg)

4. Add your Unity methods. Choose from a list of some common ones.
You can also add any custom code inside each one.

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/4.%20Add%20Unity%20Methods.jpg)

5. Add your Custom methods.
Add return types or any parameters and custom code as well.

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/5.%20Add%20Custom%20Methods.jpg)

6. Press "Generate" and see the final result.

![name](https://github.com/andreastmedia/Unity_ScriptGenerator/blob/main/0%20-%20Examples%20-%200/6.%20Final%20Result.jpg)

Hope you like it! :)
