<template>
  <v-app>
    <v-main>
      <v-navigation-drawer class="menu" temporary v-model="exibirMenu" v-if="navegacao">
        <v-list-item>
          <v-btn icon="mdi-menu" elevation="0" @click.stop="exibirMenu = !exibirMenu"></v-btn>
          Gestão de Patrimônio
        </v-list-item>
        <v-divider></v-divider>
        <v-list nav>
          <v-list-item v-for="item in itensMenu" :key="item.titulo" :prepend-icon="item.icone" :title="item.titulo"
            :to="item.rota"></v-list-item>
        </v-list>
      </v-navigation-drawer>
      <v-app-bar elevation="4" v-if="navegacao">
        <v-app-bar-nav-icon @click.stop="exibirMenu = !exibirMenu"></v-app-bar-nav-icon>
        <div>Gestão de Patrimônio</div>
      </v-app-bar>
      <router-view />

    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',


  data: () => ({
    items: [
      {
        titulo: "Home",
        rota: "/",
        icone: "mdi-home"
      },
      {
        titulo: "Usuarios",
        rota: "/usuario",
        icone: "mdi-account"
      },
      {
        titulo: "Grupos",
        rota: "/grupo",
        icone: "mdi-file-multiple"
      },
      {
        titulo: "Sair",
        rota: "/login",
        icone: "mdi-logout"
      }
    ],
    exibirMenu: false,
    permissao: ''
  }),
  beforeMount() {
  },
  moounted() {
  },
  computed: {
    navegacao() {
      const disabledNavRoutes = ['Login'];
      var retorno = true;

      this.permissao = localStorage.getItem("permissao");

      if (this.$route.name === undefined || disabledNavRoutes.indexOf(this.$route.name) != -1) {
        retorno = false;
      }
      return retorno;
    },
    itensMenu(){
      if(this.permissao != "Admin"){
        return  this.items.filter(x => {return x.titulo != "Usuarios"})
      }
      return this.items
    }
  },
  methods: {
  }
}
</script>

<style>
.cartao-login {
  display: block;
  margin: 20px auto;
  max-width: 400px;
}

.cartao-principal {
  display: block;
  margin: 20px auto;
  max-width: 1200px;
  min-width: 200px;
}

.cartao-login-titulo {
  font-size: 1.6em;
  font-weight: normal;
  width: 100%;
}

.cartao-login-actions {
  justify-content: center;
}

.center-screen {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
  min-height: 100vh;
}

.menu {
  position: absolute;
  top: 0px;
  left: 0px;
}
</style>
