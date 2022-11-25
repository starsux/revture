/*:
 *
 * @plugindesc Title screen menu
 * @author Naptora
 *
 * @param Start Text
 * @desc Set text of "Start" button
 * Default: Start
 * @default Start
 *
 * @param New Game Text
 * @desc Set text of "New game" button
 * Default: New game
 * @default New game
 *
 * @param Options Text
 * @desc Set text of "Options" button
 * Default: Options
 * @default Options
 * 
 */

var params = PluginManager.parameters("Naptora_TitleCommand");


// Create scene
function Scene_TitleCommandScene() {
  this.initialize.apply(this, arguments);
}

Scene_TitleCommandScene.prototype = Object.create(Scene_MenuBase.prototype);
Scene_TitleCommandScene.prototype.constructor = Scene_TitleCommandScene;

Scene_TitleCommandScene.prototype.initialize = function(){
  Scene_MenuBase.prototype.initialize.call(this);
}

Scene_TitleCommandScene.prototype.create = function(){
  Scene_MenuBase.prototype.create.call(this);
  this._CustomCommand_Window = new Window(0,0,320 ,240);
  this.addWindow(this._CustomCommand_Window);
}

// Create window
function TitleCommandMenu() {
  this.initialize.apply(this, arguments);
}

TitleCommandMenu.prototype = Object.create(Window_Base.prototype);
TitleCommandMenu.prototype.constructor = TitleCommandMenu;

TitleCommandMenu.prototype.initialize = function(x, y, width, height) {
  Window_Base.prototype.initialize.call(this, x, y, width, height);
};