<template>
    <section class="chartRoom">
        <el-row class="search-row el-form el-form--inline inputBox" style="float: left;width: 95%;background: #fafafa">
            <el-col :span="24" style="margin-top: 10px;padding-left: 10px;float: left">
                <label>公寓:</label>
                <el-select v-model="selectedApartment">
                    <el-option v-for="item in apartments" :key="item.id" :label="item.name" :value="item.id"></el-option>
                </el-select>
            </el-col>
            <el-col :span="24" style="padding: 0 10px;box-sizing: border-box;float: left">
                <el-col :span="14" class="statusBox">
                    <el-row :gutter="20">
                        <el-col :span="6" class="avarter">
                            <img src="./../../../assets/images/home.png" alt>
                            <p>{{apartmentName}}</p>
                        </el-col>
                        <el-col v-if="chartData.roomCount > 0" :span="9" style="margin-left: 0;margin-right: 0;">
                            <el-col :span="12" class="percent">
                                <el-progress type="circle" :width="90" :percentage="roomPercent" color="#32D088" class="progress"></el-progress>
                            </el-col>
                            <el-col v-if="chartData.roomCount > 0" :span="12" class="percentContent">
                                <p class="titlerate">空房/总房</p>
                                <p class="titlerate_">{{chartData.emptyRoomCount}}/{{chartData.roomCount}}</p>
                            </el-col>
                        </el-col>
                        <el-col :span="9" style="margin-left: 0;margin-right: 0;padding-left: 0;">
                            <el-col v-if="chartData.bedCount > 0" :span="12" class="percent">
                                <el-progress type="circle" :width="90" :percentage="bedPercent" color="#32CEE4" class="progress"></el-progress>
                            </el-col>
                            <el-col v-if="chartData.bedCount > 0" :span="12" class="percentContent">
                                <p class="titlerate">空床/总床位</p>
                                <p class="titlerate_">{{chartData.emptyBedCount}}/{{chartData.bedCount}}</p>
                            </el-col>
                        </el-col>
                    </el-row>
                </el-col>
                <el-col :span="10" class="status_">
                    <el-col :span="24" class="border_bottom">
                        状态：
                        <span v-for="(item,index) in status" :key="index" class="status_span" @click="handleStatusChange(index)" :class="{ activeClick:currentStatus == index}">{{item}}</span>
                    </el-col>
                    <el-col :span="24" class="border_bottom">
                        房型：
                        <span :key="index" v-for="(item,index) in roomTypes" class="status_span" @click="handleRoomTypeChange(item.id,index)" :class="{ activeClick:currentRoomTypeIndex == index}">{{item.name}}</span>
                    </el-col>
                    <!--<el-col :span="24" class="border_bottom">-->
                    <!--入住：-->
                    <!--<span v-for="(item,index) in atHotel" class="status_span" @click="handleStayStatusChange(index)"-->
                    <!--:class="{ activeClick:currentAtHotel == index}">{{item}}</span>-->
                    <!--</el-col>-->
                    <el-col :span="24" class="border_bottom">
                        楼层：
                        <el-cascader :options="chartData.buildings" v-model="floors" @change="handleChange"></el-cascader>
                    </el-col>
                </el-col>
            </el-col>

            <el-col :span="24" class="houseBox" style="float: left">
                <div @click="open">
                    <el-col :span="1" class="houseCard" :key="room.name" v-for="room in rooms" :class="roomClasses[room.roomStatus]">
                        <el-col :span="24" class="houseNum">房号：{{room.name}}</el-col>
                        <el-col :span="24" class="houseType">{{room.roomType && room.roomType.name}}</el-col>
                    </el-col>
                </div>
            </el-col>
            <el-col :span="24" style="margin-top: 20px;padding-bottom: 20px;float: left">
                <el-col :span="12" style="text-align:right;">
                    <el-button style="background:#42b983;color: #fff ">空房</el-button>
                    <el-button type="warning">已入住</el-button>
                    <el-button type="danger">已住满</el-button>
                </el-col>
            </el-col>
        </el-row>
        <el-row style="float: right;width: 5%;background: #fafafa">
            <el-col :span="1" class="floorsBox">
                <p class="floors">
                    <!--<i class="fa fa-building" aria-hidden="true"></i>-->
                    楼层
                </p>
                <p class="floors" :key="index" :class="choosedFloor === index ?'activeClick':''" v-for="(floornumber,index) in floorNum" @click="chooseFloor(index)">{{floornumber}}</p>
            </el-col>
        </el-row>

        <el-dialog title="房间入住信息" :visible.sync="dialogVisible" width="30%" :before-close="handleClose">
            <p>床位号1：张三</p>
            <p>床位号2：李四</p>
        </el-dialog>
    </section>
</template>
<script>
import api from "../../../config/api";
import _ from "lodash";

export default {
    data() {
        return {
            choosedFloor: 0,
            floorNum: ["F1", "F2", "F3", "F4"],
            dialogVisible: false,
            status: ["全部", "空房", "已住人", "已住满"],
            roomTypes: [{ name: "全部" }],
            atHotel: ["全部", "包房", "拼房"],
            floors: [],
            currentRoomTypeIndex: 0,
            currentStatus: 0,
            currentRoomType: "",
            currentAtHotel: 0,
            isClick: "",
            tableData: [
                {
                    a: "养老服务管理",
                    b: "89",
                    c: "116",
                    d: "8",
                    e: "13"
                },
                {
                    a: "北苑",
                    b: "170",
                    c: "306",
                    d: "95",
                    e: "168"
                },
                {
                    a: "养老服务管理",
                    b: "89",
                    c: "116",
                    d: "8",
                    e: "13"
                },
                {
                    a: "养老服务管理",
                    b: "89",
                    c: "116",
                    d: "8",
                    e: "13"
                },
                {
                    a: "养老服务管理",
                    b: "89",
                    c: "116",
                    d: "8",
                    e: "13"
                }
            ],
            apartments: [],
            selectedApartment: "",
            chartData: {
                buildings: []
            },
            rooms: [],
            roomClasses: {
                1: "green",
                2: "orange",
                3: "red"
            }
        };
    },
    computed: {
        roomPercent: function () {
            let percent =
                this.chartData.emptyRoomCount / this.chartData.roomCount;
            return Math.round(percent * 100);
        },
        bedPercent: function () {
            let percent =
                this.chartData.emptyBedCount / this.chartData.bedCount;
            return Math.round(percent * 100);
        },
        apartmentName: function () {
            var selected = _.find(
                this.apartments,
                x => x.id == this.selectedApartment
            );
            return selected ? selected.name : "";
        }
    },

    mounted() {
        this.$axios
            .get(api.apartmentDropdown)
            .then(response => {
                this.apartments = response.data;
                this.selectedApartment = this.apartments[0].id;
                let url = `${api.roomChart}?apartmentId=${
                    this.selectedApartment
                    }`;
                this.$axios.get(url).then(res => {
                    this.chartData = res.data;
                    this.roomTypes = this.roomTypes.concat(
                        this.chartData.roomTypes
                    );
                });
            })
            .catch(err => {
                console.log(err.message);
            });
    },

    methods: {
        chooseFloor(index) {
            console.log(index);
            this.choosedFloor = index;
        },
        handleChange(value) {
            this.refreshRooms();
        },
        open() {
            this.dialogVisible = true;
        },
        handleClose(done) {
            this.dialogVisible = false;
        },
        handleStatusChange(index) {
            this.currentStatus = index;
            this.refreshRooms();
        },
        handleRoomTypeChange(id, index) {
            this.currentRoomType = id;
            this.currentRoomTypeIndex = index;
            this.refreshRooms();
        },
        handleStayStatusChange(index) {
            this.currentAtHotel = index;
            this.refreshRooms();
        },
        refreshRooms() {
            if (this.floors.length > 0) {
                let [bid, uid, fid] = this.floors;
                let building = _.find(
                    this.chartData.buildings,
                    x => x.value == bid
                );
                let unit = _.find(building.children, x => x.value == uid);
                let floor = _.find(unit.children, x => x.value == fid);
                if (floor && floor.rooms && floor.rooms.length > 0) {
                    let rooms = floor.rooms;
                    if (this.currentStatus > 0) {
                        if (this.currentStatus == 2) {
                            rooms = rooms.filter(x => x.roomStatus >= 2);
                        } else {
                            rooms = rooms.filter(
                                x => x.roomStatus == this.currentStatus
                            );
                        }
                    }
                    if (this.currentRoomType) {
                        rooms = rooms.filter(
                            x =>
                                x.roomType &&
                                x.roomType.id == this.currentRoomType
                        );
                    }
                    this.rooms = rooms;
                } else {
                    this.rooms = [];
                }
            }
        },
        details() {
            this.$router.push({
                name: "sales-chart-details",
                params: {
                    id: "id123"
                }
            });
        }
    }
};
</script>
<style lang="scss">
.chartRoom {
    .el-progress--circle .el-progress__text {
        font-size: 18px !important;
    }
    .el-dialog__body {
        padding-top: 5px;
    }
    ::-webkit-scrollbar {
        height: 0;
        width: 0;
        color: transparent;
    }
}
</style>
<style lang="scss" scoped>
.chartRoom {
    width: 100%;
    background: #fafafa;
    .avarter {
        background: #fff;
        height: 150px;
        position: relative;
        img {
            width: 100px;
            height: 100px;
            box-sizing: border-box;
            position: absolute;
            top: 40%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
        p {
            text-align: center;
            position: absolute;
            top: 68%;
            left: 50%;
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            font-size: 14px;
            color: #666;
        }
    }
    .percent {
        height: 150px;
        background: #ffffff;
        position: relative;
        .progress {
            /*padding: 25px;*/
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
    }
    .percentContent {
        height: 150px;
        background: #ffffff;
        position: relative;
        .titlerate {
            position: absolute;
            top: 25%;
            left: 30%;
            transform: translate(-50%, -50%);
            font-size: 12px;
        }
        .titlerate_ {
            position: absolute;
            top: 45%;
            left: 30%;
            transform: translate(-50%, -50%);
            font-size: 24px;
        }
    }
    .statusBox {
        padding: 10px;
        font-size: 14px;
        .status_span {
            display: inline-block;
            padding: 3px 45px 5px 5px;
            margin-right: 5px;
        }
    }
    .status_ {
        padding: 10px;
        height: 150px;
        margin-top: 10px;
        background: #fff;
        color: #666;
        font-size: 12px;
        .border_bottom {
            height: 33px;
            .status_span {
                margin-right: 30px;
                display: inline-block;
                padding: 2px 10px;
                cursor: pointer;
                color: #666;
            }
            .activeClick {
                border: 1px solid #409eff;
                color: #409eff;
                background: #ade3ed;
                border-radius: 3px;
            }
            .active {
                color: red;
            }
        }
    }
    .houseBox {
        margin-top: 20px;
        min-height: 400px;
        .houseCard {
            width: 120px;
            text-align: center;
            border: 1px solid #ccc;
            padding: 20px;
            border-radius: 10px;
            margin: 0 0 10px 20px;
            font-size: 12px;
            cursor: pointer;
            color: #fafafa;
            .houseNum {
            }
            .houseType {
            }
        }
        .red {
            background: #f56c6c;
        }
        .green {
            background: #42b983;
        }
        .orange {
            background: #e6a23c;
        }
    }
    .floorsBox {
        background: #fff;
        margin-right: 0px;
        width: 60px;
        height: 650px;
        overflow-y: auto;
        margin-top: 55px;
        .bulidingICon {
            color: #409eff;
            text-align: center;
            font-size: 34px;
            height: 35px;
            line-height: 35px;
            border-bottom: 1px solid #ccc;
        }
        .floors {
            border-bottom: 1px solid #ddd;
            text-align: center;
            height: 35px;
            line-height: 35px;
            cursor: pointer;
        }
        .activeClick {
            background: #6cd2e9;
            color: #fff;
        }
    }
}
</style>

