<template>
    <v-container>
        <v-row>
            <v-card class="cartao-principal">
                <v-card-title>
                    Usuários
                    <v-spacer></v-spacer>
                    <v-btn color="success" @click="cadastrar">
                        Adicionar
                    </v-btn>
                </v-card-title>
                <v-card-text>
                    <v-table>
                        <thead>
                            <tr>
                                <th class="text-left">
                                    Nome
                                </th>
                                <th class="text-left">
                                    Email
                                </th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in usuarios" :key="item.id">
                                <td>{{ item.userName }}</td>
                                <td>{{ item.email }}</td>
                                <td align="right">
                                    <v-btn color="red-darken-2" @click="excluir(item.id)">
                                        Excluir
                                    </v-btn>
                                </td>
                            </tr>
                        </tbody>
                    </v-table>
                </v-card-text>
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
        usuarios: [],
        textSnackbar: "",
        snackbar: false,
        snackbarSucesso: false,
    }),

    created() {
        this.retornaUsuarios();
    },
    methods: {
        retornaUsuarios() {
            api
                .get("/usuario/ListarUsuarios"
                )
                .then((res) => {
                    this.usuarios = res;
                })
                .catch((error) => {
                    this.textSnackbar = error;
                    this.snackbar = true;
                });
        },
        cadastrar() {
            this.$router.push("/usuario/0")
        },
        excluir(id) {
            api
                .delete("/usuario/Deletar", {
                    params: {
                        id: id
                    }
                }
                )
                .then((res) => {
                    this.textSnackbar = res;
                    this.snackbar = true;
                    this.snackbarSucesso = true;
                    this.retornaUsuarios();
                })
                .catch((error) => {
                    this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.';
                    this.snackbarSucesso = false;
                    this.snackbar = true;
                });
        }
    }
}
</script>
