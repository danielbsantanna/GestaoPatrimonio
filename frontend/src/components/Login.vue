<template>
  <div class="fundo">
    <v-container class="center-screen">
      <v-row>
        <v-col>
          <v-card class="cartao-login elevation-10">
            <v-card-title class="cartao-login-titulo">
              Login
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-text-field v-model="login" label="Usuário">
                </v-text-field>
              </v-row>
              <v-row>
                <v-text-field v-model="senha" :append-icon="mostrarSenha ? 'mdi-eye' : 'mdi-eye-off'"
                  :type="mostrarSenha ? 'text' : 'password'" @click:append="mostrarSenha = !mostrarSenha" label="Senha">
                </v-text-field>
              </v-row>
            </v-card-text>
            <v-card-actions class='cartao-login-actions'>
              <v-btn @click="realizarLogin" color="success">Acessar</v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
    <v-snackbar v-model="snackbar" timeout="5000" color="red-darken-2">
      {{ textSnackbar }}
      <template v-slot:actions>
        <v-btn icon="mdi-close" @click="snackbar = false"></v-btn>
      </template>
    </v-snackbar>
  </div>


</template>

<script>

import api from "../../api";

export default {

  data: () => ({
    login: '',
    senha: '',
    textSnackbar: "",
    snackbar: false,
    mostrarSenha: false
  }),

  methods: {
    realizarLogin() {
      api
        .post("/usuario/login", {
          login: this.login,
          senha: this.senha
        })
        .then((res) => {
          localStorage.token = res.token;
          localStorage.permissao = res.permissao;
          this.$router.push('/');
        })
        .catch((error) => {
          this.textSnackbar = error;
          this.snackbar = true;
        });
    }
  },
  mounted() {
    this.mostrarMenu = (this.$router.currentRoute._value.name != "Login")
    localStorage.token = '';
    localStorage.permissao = '';
  }
}
</script>


