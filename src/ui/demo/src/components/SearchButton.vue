<template>
    <el-row class="moreBtn">
        <span v-if="!isMore" @click="onClick()">更多&#x25BC;</span>
        <span v-if="isMore" @click="onClick()">收起&#x25B2;</span>
    </el-row>
</template>

<script>
export default {
  props: {
    value: {
      type: Boolean
    }
  },
  data() {
    return {
      isMore: false
    };
  },

  mounted() {
    this.isMore = this.$props.value;
  },

  methods: {
    onClick() {
      this.isMore = !this.isMore;
      // 根据v-model对封装的组件赋值，必须要填写
      this.$emit("input", this.isMore);
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
@import "../element-variables";
.moreBtn {
  display: table;
  white-space: nowrap;
  border-spacing: 0px 0;
  font-size: 12px;
  color: $--color-primary;
  // margin-bottom: 18px;
}

.moreBtn:before,
.moreBtn:after {
  display: table-cell;
  content: "";
  width: 50%;
  background: -webkit-linear-gradient(#eee, #eee) repeat-x left center;
  background: linear-gradient(#eee, #eee) repeat-x left center;
  background-size: 1px 1px;
}
.moreBtn {
  cursor: pointer;
}
</style>