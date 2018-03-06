<template>
  <div class="container">
    <navigation-bar></navigation-bar>
    <main></main>
  </div>
</template>

<script>
import NavigationBar from './NavigationBar'
import Main from './Main'

export default {
  name: 'Layout',
  components: {
    NavigationBar: NavigationBar,
    Main: Main
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
