<template>
    <div>
        <HeaderLayout></HeaderLayout>
        <el-main >
            <router-view/>
        </el-main>
    </div>
</template>


<script>
import HeaderLayout from "../views/HeaderLayout.vue";
import { RouteConfig, RouteRecord } from "vue-router";

export default {
  components: {
    HeaderLayout
  },
  mounted() {
    // this.$router.push("/home")
  }
};
</script>