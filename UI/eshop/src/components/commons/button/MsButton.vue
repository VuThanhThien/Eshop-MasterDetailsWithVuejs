<template>
    <button @click="isFocus()" :class="bgcolor" class="btn">
        <div v-if="icon && !iconAfter" :class="icon" class="icon-left"></div>
        <slot />
        <div v-if="icon && iconAfter" :class="icon"></div>
    </button>
</template>

<script>
export default {
    name: 'msButton',
    props: {
        icon: {
            type: String
        },
        iconAfter: {
            type: [String, Boolean],
            default: false
        },
        bgcolor: {
            type: String,
            default: false
        }
    },
    methods: {
        isFocus(){
            this.$emit('buttonEvent');        
        }
    }    
}
</script>
<style scoped>


</style>