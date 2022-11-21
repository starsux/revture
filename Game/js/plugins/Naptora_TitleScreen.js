/*:
 *
 * @plugindesc Title screen menu
 * @author Naptora
 * 
 * @param Background
 * @desc Image in bottom of scene
 * @default MenuBackground
 * @require 1
 * @dir img/menu/
 * @type file
 * 
 * @param Title Image
 * @desc Image to use as title
 * @default logo
 * @require 1
 * @dir img/menu/
 * @type file
 * 
 * @param Title Image Factor
 * @desc Image size factor (0-100)
 * Default: 100
 * @default 100
 * 
 * @param ===== Press start =====
 * @default
 * 
 * @param Press Start key
 * @desc Key for show menu
 * @default ok
 * 
 * @param Press start text
 * @desc Text for show in "Press x key for start"
 * Default: "Press enter for start"
 * @default Press enter for start
 * 
 * @param Font size
 * @desc Font size of text
 * Default: 48
 * @default 48
 * 
 * @param X Position
 * @desc Position of text in X axis
 * Default: Graphics.width/2
 * @default Graphics.width/2
 * 
 * @param Y Position
 * @desc Position of text in y axis.
 * @default Graphics.height*(3/4)
 * 
 * @param Height
 * @desc Height of text
 * Default: 48
 * @default 48
 * 
 * @param Width
 * @desc Width of text
 * Default: Graphics.width
 * @default Graphics.width
 * 
 */

var params = PluginManager.parameters("Naptora_TitleScreen");

var title_image_path = String(params["Title Image"]);
var title_factor = Number.parseInt(params["Title Image Factor"]);
var bg_image_path = String(params["Background"]);

var press_start_key = String(params["Press Start key"]);
var press_start_text = String(params["Press start text"]);
var press_start_fontsize = String(params["Font size"]);
var press_start_x = String(params["X Position"]);
var press_start_y = String(params["Y Position"]);
var press_start_width = String(params["Width"]);
var press_start_height = String(params["Height"]);



(function(){

/* rewrite ImageManager for index "menu" folder */
ImageManager.loadMenu = function(filename, hue) {
    return this.loadBitmap('img/menu/', filename, hue, false);
};

/* Rewrite scene title of native rpgmaker*/

// Set background
Scene_Title.prototype.createBackground = function() {
    this._backSprite1 = new Sprite(ImageManager.loadMenu(bg_image_path));
    this._backSprite2 = new Sprite(ImageManager.loadTitle2($dataSystem.title2Name));
    this.addChild(this._backSprite1);
};

// Set Title image
Scene_Title.prototype.createForeground = function() {
    this._gameTitleSprite = new Sprite(ImageManager.loadMenu(title_image_path));
    // Resize title image
    var factor = title_factor/100;
    this._gameTitleSprite.scale.set(factor,factor);

    this.addChild(this._gameTitleSprite);
    
    // Center title
    Scene_Title.prototype.centerSprite(this._gameTitleSprite);

    // Climb title
    this._gameTitleSprite.y = Graphics.height / 4;
    this._gameTitleSprite.anchor.y = 0.5;

    // Create text "Press key for start"
    this._startTextSprite = new Sprite(new Bitmap(Graphics.width, Graphics.height));
    this.addChild(this._startTextSprite);
    this.DrawStartText();

};

Scene_Title.prototype.DrawStartText = function(){
    var x = eval(press_start_x);
    var y = eval(press_start_y);
    var w = eval(press_start_width);
    var h = eval(press_start_height);


    this._startTextSprite.bitmap.fontSize = press_start_fontsize;
    this._startTextSprite.bitmap.drawText(press_start_text, x, y, w, h, "center");
    
}

Scene_Title.prototype.update = function() {
    if (!this.isBusy()) {
        
        // Is user press key for start?
        if(Input.isTriggered(press_start_key)){
             this._commandWindow.open();
        }
    }
    Scene_Base.prototype.update.call(this);
}


})();