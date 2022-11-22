/*:
 *
 * @plugindesc Show a Splash Screen before going to main screen.
 * @author Naptora
 *
 *
 * @param Splash File Path
 * @desc The image to use when showing the splash screen
 * Default: img/System/Splash
 * @default img/System/Splash
 * @type text
 * 
 * @param Splash File Type
 * @desc Select the type file of splash file
 * @type select
 * @option Video (webm)
 * @option Image
 *
 * @param Fade Out Time
 * @desc The time it takes to fade out, in frames.
 * Default: 120
 * @default 120
 *
 * @param Fade In Time
 * @desc The time it takes to fade in, in frames.
 * Default: 120
 * @default 120
 *
 * @param Wait Time
 * @desc The time between fading in and out, in frames.
 * Default: 160
 * @default 160
 * 
 * @param Image Factor
 * @desc Change image size maintaining the aspect ratio
 * Default 100%
 * @default 100%
 *
 */

var Plg_mang = Plg_mang || {};
Plg_mang.SplashScreen = {};
Plg_mang.SplashScreen.Parameters = PluginManager.parameters('Naptora_SplashScreen');

Plg_mang.SplashScreen.SplImage = String(Plg_mang.SplashScreen.Parameters["Splash File Path"]);
Plg_mang.SplashScreen.SplashType = Plg_mang.SplashScreen.Parameters["Splash File Type"];

Plg_mang.SplashScreen.FadeOutTime = Number(Plg_mang.SplashScreen.Parameters["Fade Out Time"]) || 120;
Plg_mang.SplashScreen.FadeInTime = Number(Plg_mang.SplashScreen.Parameters["Fade In Time"]) || 120;
Plg_mang.SplashScreen.WaitTime = Number(Plg_mang.SplashScreen.Parameters["Wait Time"]) || 160;
Plg_mang.SplashScreen.ImageFactor = Number.parseInt(Plg_mang.SplashScreen.Parameters['Image Factor']);

//-----------------------------------------------------------------------------
// Scene_Splash
//
// This is a constructor, implementation is done in the inner scope.

function Scene_Splash() {
    this.initialize.apply(this, arguments);
}

(function() {

    

    //-----------------------------------------------------------------------------
    // Scene_Boot
    //
    // The scene class for dealing with the game boot.
    
    var _Scene_Boot_loadSystemImages = Scene_Boot.prototype.loadSystemImages;
    Scene_Boot.prototype.loadSystemImages = function() {
        _Scene_Boot_loadSystemImages.call(this);
        ImageManager.loadSystem(Plg_mang.SplashScreen.SplImage);

    };

    var _Scene_Boot_start = Scene_Boot.prototype.start;
    Scene_Boot.prototype.start = function() {
        if (!DataManager.isBattleTest() && !DataManager.isEventTest()) {
            SceneManager.goto(Scene_Splash);
        } else {
            _Scene_Boot_start.call(this);
        }
    };

    //-----------------------------------------------------------------------------
    // Scene_Splash
    //
    // The scene class for dealing with the splash screens.

    Scene_Splash.prototype = Object.create(Scene_Base.prototype);
    Scene_Splash.prototype.constructor = Scene_Splash;

    Scene_Splash.prototype.initialize = function() {
        Scene_Base.prototype.initialize.call(this);
        this._mvSplash = null;
        this._customSplash = null;
        this._mvWaitTime = Plg_mang.SplashScreen.WaitTime;
        this._customWaitTime = Plg_mang.SplashScreen.WaitTime;
        this._mvFadeOut = false;
        this._mvFadeIn = false;
        this._customFadeOut = false;
        this._customFadeIn = false;
    };

    Scene_Splash.prototype.create = function() {
        Scene_Base.prototype.create.call(this);
        this.createSplashes();
    };

    Scene_Splash.prototype.start = function() {
        Scene_Base.prototype.start.call(this);
        SceneManager.clearStack();
        if (this._mvSplash != null) {
            this.resizeSplash(this._mvSplash);
            this.centerSprite(this._mvSplash);
        }

    };

    Scene_Splash.prototype.update = function() {
        // Fade In/Out image
        // if (!this._mvFadeIn) {
        //     this.startFadeIn(Plg_mang.SplashScreen.FadeInTime, false);
        //     this._mvFadeIn = true;
        // } else {
        //     if (this._mvWaitTime > 0 && this._mvFadeOut == false) {
        //         this._mvWaitTime--;
        //     } else {
        //         if (this._mvFadeOut == false) {
        //             this._mvFadeOut = true;
        //             this.startFadeOut(Plg_mang.SplashScreen.FadeOutTime, false);
        //         }
        //     }
        // }
        // if (this._mvFadeOut == true) {
        //     this.gotoTitleOrTest();
        // }

        //? Video is not playing?
        if(!Graphics.isVideoPlaying()){
            // Go to title scene
            this.gotoTitleOrTest();
        }

        Scene_Base.prototype.update.call(this);
    };

    Scene_Splash.prototype.createSplashes = function() {

        // Selected is an image?
        if(Plg_mang.SplashScreen.SplashType.toUpperCase().includes("IMAGE")){
            this._mvSplash = new Sprite(ImageManager.loadNormalBitmap(Plg_mang.SplashScreen.SplImage));
                // Add to scene            
                this.addChild(this._mvSplash);
        }else{

            // Play video
            var vid = Graphics.playVideo(Plg_mang.SplashScreen.SplImage);
            console.log(vid);
        }
    };

    Scene_Splash.prototype.resizeSplash = function(sprite) {
        if(Plg_mang.SplashScreen.ImageFactor != 100){

            var factor = Plg_mang.SplashScreen.ImageFactor/100;
            
            // Apply scale
            sprite.scale.set(factor,factor);
            
        }
    };

    Scene_Splash.prototype.centerSprite = function(sprite) {

        sprite.x = Graphics.width / 2;
        sprite.y = Graphics.height / 2;
        sprite.anchor.x = 0.5;
        sprite.anchor.y = 0.5;

        
    };

    Scene_Splash.prototype.gotoTitleOrTest = function() {
        Scene_Base.prototype.start.call(this);
        SoundManager.preloadImportantSounds();
        if (DataManager.isBattleTest()) {
            DataManager.setupBattleTest();
            SceneManager.goto(Scene_Battle);
        } else if (DataManager.isEventTest()) {
            DataManager.setupEventTest();
            SceneManager.goto(Scene_Map);
        } else {
            this.checkPlayerLocation();
            DataManager.setupNewGame();
            SceneManager.goto(Scene_Title);
            Window_TitleCommand.initCommandPosition();
        }
        this.updateDocumentTitle();
    };

    Scene_Splash.prototype.updateDocumentTitle = function() {
        document.title = $dataSystem.gameTitle;
    };

    Scene_Splash.prototype.checkPlayerLocation = function() {
        if ($dataSystem.startMapId === 0) {
            throw new Error('Player\'s starting position is not set');
        }
    };

})();