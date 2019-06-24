import Vuex from "vuex";
import Vue from "vue";
import * as types from "./types";
import VuexPersist from "vuex-persist";

Vue.use(Vuex);
const vuexPersist = new VuexPersist({
    key: "my-app",
    storage: localStorage
});
export default new Vuex.Store({
    state: {
        user: null,
        token: null,
        title: ""
    },
    mutations: {
        [types.LOGIN]: (state, data) => {
            state.token = data.token;
            state.user = data.user;
        },
        [types.LOGOUT]: state => {
            state.token = null;
            state.user = null;
        }
    },
    plugins: [vuexPersist.plugin]
});
