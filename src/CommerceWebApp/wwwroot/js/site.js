/* Dashboard */
$(document).ready(function() {
    $(".zero-triggered").hide();
    var zeroTimesTriggeredShowing = false;
    $("input[name='hideZeroesCheckbox']").bind("click", function() {
        if(!zeroTimesTriggeredShowing) {
            $(".zero-triggered").show();
            zeroTimesTriggeredShowing = true;
        }
        else {
            $(".zero-triggered").hide();
            zeroTimesTriggeredShowing = false;
        }
    });
});