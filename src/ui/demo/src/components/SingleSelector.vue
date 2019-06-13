<template>
    <div>
        <a v-for="item in data" href="javascript:;" class="filter-item" :class="item.value === value ? 'active': ''" :key="item.value" @click="select(item)">
            {{item.name}}
        </a>
    </div>
</template>

<script>
export default {
    props: {
        data: {
            type: Array,
            default: function() {
                return [];
            }
        },
        value: {
            type: [String, Number]
        }
    },
    methods: {
        select(item) {
            this.$emit("input", item.value);
            this.$emit("selected", item.value);
        }
    }
};
</script>

