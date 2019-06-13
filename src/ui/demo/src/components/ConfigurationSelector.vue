<template>
    <el-select v-if="types" :value="value" @change="handleChange">
        <el-option
            v-for="item in types"
            :key="item.value"
            :label="item.text"
            :value="item.value"
        >{{item.text}}</el-option>
    </el-select>
</template>

<script>
import areas from "../common/Areas";
import { isArray } from "util";
import Configurations from "../common/Configurations";

export default {
    props: {
        value: {
            type: Number
        },
        type: {
            type: String
        },
        name: {
            type: String
        }
    },

    data() {
        return {
            types: ""
        };
    },

    methods: {
        handleChange(val) {
            this.$emit("input", val);
            this.$emit("change", val);
        }
    },

    async mounted() {
        let types = await Configurations.getConfiguration(this.type, this.name);
        this.types = types;
    }
};
</script>
