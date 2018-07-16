/*  
    js源代码.
    Description: The js is for testing require.
 */
define(function () {
    function RequireIndexViewModel() {
        var self = this;

        self.TestRequireJs = function () {
            alert("The function is in RequireIntroduction.js!");
        }
    }

    return new RequireIndexViewModel();
});