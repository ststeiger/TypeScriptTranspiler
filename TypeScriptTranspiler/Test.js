var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var Greeter = (function () {
    function Greeter(message) {
        this.greeting = message;
    }
    Greeter.prototype.greet = function () {
        return "Hello, " + this.greeting;
    };
    return Greeter;
})();
var greeter = new Greeter("world");
var button = document.createElement('button');
button.textContent = "Say Hello";
button.onclick = function () {
    alert(greeter.greet());
};
document.body.appendChild(button);
//function add(a:number, b:number) : number
function add(a, b) {
    return a + b;
}
var ele = document.getElementById("foobar");
var x = add(ele.value, 123);
var COR;
(function (COR) {
    var foo = (function () {
        function foo(name) {
            this.name = name;
        }
        foo.prototype.move = function (meters) {
            alert(this.name + " moved " + meters + "m.");
        };
        return foo;
    })();
    var bar = (function (_super) {
        __extends(bar, _super);
        function bar(name) {
            _super.call(this, name);
        }
        bar.prototype.makeSound = function (input) {
            throw new Error('This method is not yet implemented.');
        };
        return bar;
    })(foo);
    var StandardEngine = (function () {
        function StandardEngine(size, turbo) {
            this.size = size;
            this.turbo = turbo;
        }
        StandardEngine.prototype.getSize = function () {
            return this.size;
        };
        StandardEngine.prototype.getTurbo = function () {
            return this.turbo;
        };
        return StandardEngine;
    })();
})(COR || (COR = {}));
