<template>
    <v-container>
        <v-row>
            <v-card class="cartao-principal">
                <v-card-title>
                    Notas
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
                                    Id
                                </th>
                                <th class="text-left">
                                    Descrição
                                </th>
                                <th class="text-left">
                                    Grupo
                                </th>
                                <th class="text-left">
                                    Quantidade
                                </th>
                                <th class="text-left">
                                    Valor
                                </th>
                                <th class="text-left">
                                    Data Emissão
                                </th>
                                <th class="text-left">
                                    Data Entrada
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in notas" :key="item.id">
                                <td>{{ item.id }}</td>
                                <td>{{ item.descricao }}</td>
                                <td>{{ item.grupoId }}</td>
                                <td>{{ item.quantidade }}</td>
                                <td>{{ item.valor }}</td>
                                <td>{{ item.dataEmissao }}</td>
                                <td>{{ item.dataEntrada }}</td>
                            </tr>
                        </tbody>
                    </v-table>
                </v-card-text>
            </v-card>
        </v-row>
    </v-container>
</template>

<script>

import api from "../../api";

export default {

    data: () => ({
        notas: []
    }),

    created() {
        this.retornaNotas();
    },
    methods: {
        retornaNotas() {
            api
                .get("/nota/Listar"
                )
                .then((res) => {
                    this.notas = res;
                })
                .catch((error) => {
                    //  console.log(error);
                });
        },
        cadastrar() {
            this.$router.push("/nota/0")
        }
    }
}
</script>
