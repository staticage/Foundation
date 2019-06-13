class Areas {
    static instance;

    static getInstance() {
        if (!this.instance) {
            this.instance = require("../common/pcas.json");
        }
        return this.instance;
    }
}

const areas = Areas.getInstance();

export default areas;
