import VueRouter, { RouteConfig } from "vue-router";

export class Routes {
    static getCurrentRootRoute(router) {
        const routes = router.options.routes;
        return routes.filter(x => x.path === router.currentRoute.matched[0].path)[0];
    }
}
