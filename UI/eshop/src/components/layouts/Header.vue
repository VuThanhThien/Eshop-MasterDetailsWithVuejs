<template>
     <div class="header">
         
         <!-- tiêu đề  -->
        <div class="title">Hàng hóa</div>

        <!-- thông tin tài khoản  -->
        <div class="accoutInforBox">
            <div class="backPrevPage">
                <div class="iconBack"></div>
                <router-link to="/Home">
                    <h4>Quay lại trang bắt đầu sử dụng</h4>
                </router-link>
            </div>
            <!-- chọn nhà hàng hiện tại  -->
            <select id="myRestaurant">
                <option>DODAVI- Cầu Giấy</option>
                <option>DODAVI- Nam Từ Liêm</option>
            </select>
            <!-- tài khoản hiện tại  -->
            <div class="accountInfor">
                <div class="accountAvatar"></div>
                <select class="accountName">
                    <option >vtthien</option>
                </select>
                <!-- <div class="accountName">qtht misa</div> -->
                <!-- <div class="accountOption"></div> -->
            </div>
            <!-- nút gọi  -->
            <div class="callMe"></div>
            <!-- chuông  -->
            <div  class="bell">
                <div class="fa fa-bell fa-2x"></div>
            </div>
            <div class="question">
                <i id="iconquestion" class="fa fa-question-circle fa-2x" aria-hidden="true"></i>
            </div>
        </div>
        
    </div>
</template>
<style scoped>
.bell{
    margin: 0 15px 0 15px;
    color: #2b3173;
}
.question{
    height: 60px;
    width: 60px;
    align-items: center;
    border-left: 1px solid rgb(190, 189, 189);
}
#iconquestion{
    color: #2b3173;
    padding: 14px;
}
.backPrevPage{
    padding-right: 15px;
    margin-right: 15px;
    border-right: solid 1px #434344;
}
.backPrevPage{
    align-items: center;
    justify-content: space-between;
    display: flex;
    height: 30px;
}
.iconBack{
    background: url("../../assets/common-icon.png") no-repeat -454px -129px;
    width: 11px;
    height: 11px;
    margin-right: 15px;
}
</style>