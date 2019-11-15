<template>
    <div id="app">
        <h2>Hello Vue. {{message}}</h2>
        <h2>Hello Vue. {{message2}}</h2>
        <h2>Hello Vue. {{message3}}</h2>
        <h2>Hello Vue. {{message4}}</h2>
    </div>
</template>
<script>
  export default {
    name: 'app',
    data: function () {
      return {
          message: "ok.",
          message2: "ok2.",
          message3:"ok3.",
          message4:"ok4.",
      }
    }
  }
</script>