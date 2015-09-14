
class Greeter {
    greeting: string;
    constructor(message: string) {
        this.greeting = message;
    }
    greet() {
        return "Hello, " + this.greeting;
    }
}

var greeter = new Greeter("world");

var button : HTMLButtonElement = <HTMLButtonElement>document.createElement('button');
button.textContent = "Say Hello";
button.onclick = function() {
    alert(greeter.greet());
}

document.body.appendChild(button);



//function add(a:number, b:number) : number
function add(a, b)
{
	return a+b;
}


var ele:HTMLInputElement = <HTMLInputElement>document.getElementById("foobar");
var x:number = add(ele.value, 123);






namespace COR 
{
	abstract class foo
	{
		protected name:string;
		
		constructor(name: string){ this.name = name;}
		
		 abstract makeSound(input : string) : string;
		 
		 move(meters) {
			 alert(this.name + " moved " + meters + "m.");
		 }
	}
	
	
	class bar extends foo
	{
		constructor(name: string) { super(name); }
		
		makeSound(input : string) : string {
        throw new Error('This method is not yet implemented.');
    }
	}
	
	
	interface Engine {
	  getSize(): number;
	  getTurbo(): boolean;
	}
	
	
	class StandardEngine implements Engine {
	  constructor(private size: number, private turbo: boolean) {
	  }
	  public getSize(): number {
	    return this.size;
	  }
	  public getTurbo(): boolean {
	    return this.turbo;
	  }
	}	
	
}
