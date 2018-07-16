define(['Routes', 'director'], function (Routes, Router) {
    var router = new Router(Routes);
    router.init();
    return router;
});