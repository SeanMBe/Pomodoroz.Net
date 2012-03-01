/// <reference path="jquery-1.7.1.js" />

// --------------------------------------------------
// application vars
var bellSound;


// --------------------------------------------------
// need to be able to refresh our page parts
function updateDisplay() {
    $("#running-pomodoro").load('Activity/RunningPomodoro');
    $("#action-list").load('Activity/LoadActivities');
    
}

// --------------------------------------------------
// sets up the soundManager library 
// so that we can play the bell sound
function setupAudio() {

    soundManager.url = '../../scripts/swf/';
    soundManager.flashVersion = 8;
    soundManager.useFlashBlock = false;

    soundManager.onready(function () {

        bellSound = soundManager.createSound({
            id: 'theBellSound',
            url: '../../content/audio/bell.mp3'
        });
    });
}


$(function() {

    $.ajaxSetup({ cache: false });
    setupAudio();

});
