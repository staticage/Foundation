import Vue from "vue";

class SingletonBus {
    static instance;

    static getInstance() {
        if (!this.instance) {
            this.instance = new Vue();
        }
        return this.instance;
    }
}

const bus = SingletonBus.getInstance();

export default bus;
