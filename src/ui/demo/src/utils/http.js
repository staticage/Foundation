import axios from "axios";

import router from "../router";

axios.defaults.timeout = 1000 * 60 * 3;

// axios.defaults.baseURL = "http://localhost:5500/api/";

axios.defaults.baseURL = "http://148.70.19.204:8080/api/";
axios.interceptors.request.use(
    config => {
        const token = localStorage.getItem("token");

        if (token) {
            config.headers.Authorization = `bearer ${token}`;
        }

        if ((config.method === "put" || config.method === "post") && localStorage.getItem("user") !== null) {
            const user = JSON.parse(localStorage.getItem("user"));

            if (user && config.data !== undefined && !config.data.companyId) {
                config.data.companyId = user.companyId;
            }
        }

        return config;
    },

    err => {
        return Promise.reject(err);
    }
);

axios.interceptors.response.use(
    response => {
        return response;
    },

    error => {
        if (error.response) {
            const status = error.response.status;
            if (status == 401) {
                const current = router.currentRoute.path;
                if (current !== "/login") {
                    router.replace({
                        path: "/login",

                        query: {
                            redirect: current
                        }
                    });
                }
            } 
            // else if (status === 403) {
            //     router.push("/exception/403");
            // } else if (status <= 504 && status >= 500) {
            //     router.push("/exception/500");
            // } else if (status >= 404 && status < 422) {
            //     router.push("/exception/404");
            // } 
            else {
                if (error.response.data && error.response.data.Message) {
                    return Promise.reject(new Error(error.response.data.Message));
                }
                return Promise.reject(error);
            }
        } else {
            //router.push("/exception/500");
            return Promise.reject(error);
        }
        // console.log(JSON.stringify(error));//console : Error: Request failed with status code 402
    }
);

export default axios;
