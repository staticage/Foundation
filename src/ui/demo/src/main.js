import Vue from "vue";
import App from "./App.vue";
import ElementUI from "element-ui";
import "./theme.scss";
import router from "./router";
import store from "./store/store";
import moment from "moment";
import axios from "./utils/http";

import echarts from "echarts";

Vue.prototype.$echarts = echarts;

moment.locale("zh-cn");
Vue.prototype.moment = moment;
Vue.config.productionTip = false;
Vue.use(ElementUI, {
    size: "small",
    zIndex: 3000
});
Vue.prototype.$axios = axios;
Vue.prototype.$hasPermission = function(name) {
    const user = this.$store.state.user;
    if (user.superAdmin || user.companyAdmin) {
        return true;
    }
    const permission = this.$router.currentRoute.meta.permission;
    const permissions = convert(permission).map(x => accesses[x]);
    return permissions.indexOf(name) >= 0;
};

new Vue({
    router,
    store,
    render: h => h(App),
    mounted() {
        this.checkLogin();
    },
    watch: {
        $route: "checkLogin"
    },
    methods: {
        checkLogin() {
            if (!this.$store.state.user) {
                this.$router.push("login");
            }
        }
    }
}).$mount("#app");
