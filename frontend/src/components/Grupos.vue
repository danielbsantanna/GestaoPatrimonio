<template>
    <v-container>
        <v-row>
            <v-card class="cartao-principal">
                <v-card-title>
                    Grupos
                    <v-spacer></v-spacer>
                    <v-btn color="success" @click="cadastrar" v-if="permissao == 'Admin'">
                        Adicionar
                    </v-btn>
                </v-card-title>
                <v-card-text>
                    <v-table>
                        <thead>
                            <tr>
                                <th class="text-left">
                                    Id
                                </th>
                                <th class="text-left">
                                    Nome
                                </th>
                                <th class="text-left">
                                    Vida Útil
                                </th>
                                <th class="text-left">
                                    Depreciação Mensal
                                </th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in grupos" :key="item.id">
                                <td>{{ item.id }}</td>
                                <td>{{ item.nome }}</td>
                                <td>{{ item.vidaUtil }} Meses</td>
                                <td>{{ item.depreciacaoMensal }}%</td>
                                <td align="right">
                                    <v-btn color="warning" @click="editar(item.id)">
                                        Visualizar
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
        grupos: [],
        textSnackbar: "",
        snackbar: false,
        snackbarSucesso: false,
        permissao: "Usuario"
    }),

    created() {
        this.retornaGrupos();
    },
    methods: {
        retornaGrupos() {
            api
                .get("/grupo/Listar"
                )
                .then((res) => {
                    this.grupos = res;
                })
                .catch((error) => {
                    this.textSnackbar = error;
                    this.snackbar = true;
                });
        },
        cadastrar() {
            this.$router.push("/grupo/0")
        },
        editar(id) {
            this.$router.push("/grupo/" + id)
        },
        excluir(grupo) {
            api
                .delete("/grupo/Deletar", {
                    data: grupo

                })
                .then((res) => {
                    this.textSnackbar = res;
                    this.snackbar = true;
                    this.snackbarSucesso = true;
                    this.retornaGrupos();
                })
                .catch((error) => {
                    this.textSnackbar = error ? error : 'Ocorreu um erro interno do sistema.';
                    this.snackbarSucesso = false;
                    this.snackbar = true;
                });
        }
    },
    mounted() {
        this.permissao = localStorage.getItem("permissao")
    }
}
</script>
