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
 * @param Start key
 * @desc Key for show menu
 * @default z
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
 */

var params = PluginManager.parameters("Naptora_TitleScreen");
var title_image_path = String(params["Title Image"]);
var bg_image_path = String(params["Background"]);
var title_factor = Number.parseInt(params["Title Image Factor"]);

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
};




})();