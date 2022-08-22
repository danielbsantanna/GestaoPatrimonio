<template>
  <v-container>
    <v-row class="text-center">
      <v-card class="cartao-principal">
        <v-card-title>
          <v-btn icon="mdi-arrow-left" elevation="0" @click="$router.go(-1)"></v-btn>
          Cadastro de Usuários
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text>
          <v-row>
            <v-col cols="12" sm="4">
              <v-text-field v-model="usuario.login" label="Usuário">
              </v-text-field>
            </v-col>
            <v-col cols="12" sm="4">
              <v-text-field v-model="usuario.senha" label="Senha">
              </v-text-field>
            </v-col>
            <v-col cols="12" sm="4">
              <v-text-field v-model="usuario.email" label="E-mail">
              </v-text-field>
            </v-col>
            <v-col cols="12" sm="4">
              <v-select :items="listaPermissao" v-model="usuario.permissao" label="Permissão"></v-select>
            </v-col>
          </v-row>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions class='cartao-login-actions'>
          <v-btn @click="criarUsuario">Cadastrar</v-btn>
        </v-card-actions>
      </v-card>
    </v-row>
    <v-snackbar v-model="snackbar" timeout="5000" :color="snackbarSucesso ? 'success' : 'red-darken-2'">
      {{ textSnackbar }}
      <template v-slot:actions>
        <v-btn icon="mdi-close" @click="snackbar = false"></v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script>

import api from "../../api";

export default {

  data: () => ({
    usuario: {
      login: '',
      senha: '',
      email: '',
      permissao: 'Usuario'
    },
    listaPermissao: ['Usuario', 'Admin'],
    textSnackbar: "",
    snackbar: false,
    snackbarSucesso: false,
  }),
  methods: {
    criarUsuario() {
      api
        .post("/usuario/CriarUsuario", this.usuario)
        .then((res) => {
          this.textSnackbar = res;
          this.snackbar = true;
          this.snackbarSucesso = true;
        })
        .catch((error) => {
          this.textSnackbar = error? error : 'Ocorreu um erro interno do sistema.';
          this.snackbar = true;
          this.snackbarSucesso = false;
        });
    }
  }
}
</script>
