import api from "../config/api";
import _ from "lodash";
import axios from '../utils/http';

class Configurations {
    static instance;

    static getInstance() {
        if (!this.instance) {
            this.instance = new Configurations();
        }
        return this.instance;
    }

    allConfigurations = null;

    async getConfiguration(type,name) {
        if (!this.allConfigurations) {
            this.allConfigurations = (await axios.get(api.setting)).data;
        }
        const setting = _.find(this.allConfigurations, x => x.key === type);
        if(setting) {
            let child = _.find(setting.children,x=>x.name === name);
            if(child) {
                if(child.name == "客户级别"){
                    child.items = child.items.filter(x=>!_.includes([1,3,4],x.value));
                }
                return child.items;
            }
            return [];
        }
        return [];
    }
}

const Conf = Configurations.getInstance();

export default Conf;
