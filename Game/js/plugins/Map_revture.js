/*:
* @plugindesc Floating World Map 
* @author 7b
* @help
* Ask
*/

var plugin = $plugins.filter(function(p) {
    return p.description.contains('<UniqueID>') && p.status
  })[0];