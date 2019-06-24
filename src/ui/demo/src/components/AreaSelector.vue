<template>
    <el-cascader
        :props="cascaderProps"
        :options="options"
        :value="value"
        @change="handleChange"
        @active-item-change="handleItemChange"
    ></el-cascader>
</template>

<script>
import areas from "../common/Areas";
import { isArray } from "util";

export default {
    props: {
        value: {
            type: Array
        }
    },

    data() {
        return {
            isMore: false,
            options: [],
            cascaderProps: {
                value: "label"
            }
        };
    },

    methods: {
        handleChange(val) {
            this.$emit("handleChange", val);
            this.$emit("input", val);
        },

        handleItemChange(val) {
            if (val.length < 4) {
                const item = this.getItem({ children: this.options }, [...val]);
                if (this.value) {
                    const area = this.getArea(val);
                    item.children = area;
                } else {
                    if (item.children && item.children.length === 0) {
                        const area = this.getArea(val);
                        item.children = area;
                    }
                }
            }
        },
        getArea(val) {
            let area = areas;
            for (const name of val) {
                area = area[name];
            }
            if (isArray(area)) {
                return area.map(x => ({ label: x }));
            } else {
                return Object.keys(area).map(x => ({ label: x, children: [] }));
            }
        },

        getItem(item, val) {
            if (!val.length) {
                return item;
            }

            const matched = item.children.filter(x => x.label === val[0])[0];
            return this.getItem(matched, val.splice(1));
        },

        mapAddress([a, b, c, d]) {
            let areaArr = areas;
            var optinos = Object.keys(areaArr).map(x => ({
                label: x,
                children: a
                    ? Object.keys(areaArr[a]).map(y => ({
                          label: y,
                          children: b
                              ? Object.keys(areaArr[a][b]).map(z => ({
                                    label: z,
                                    children: c
                                        ? areaArr[a][b][c].map(w => ({
                                              label: w
                                          }))
                                        : []
                                }))
                              : []
                      }))
                    : []
            }));

            return optinos;
        }
    },
    updated() {},
    mounted() {
        this.$watch("value", newValue => {
            this.options = this.value
                ? this.mapAddress(this.value)
                : this.getArea([]);
        });

        this.options = this.value
            ? this.mapAddress(this.value)
            : this.getArea([]);
    }
};
</script>
