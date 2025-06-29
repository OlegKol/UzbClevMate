Vue.component('custom-select', {
    template: `<div class="custom-select">
                    <input :disabled="isdisabled" placeholder="Начните вводить название (минимум 3 символа)" v-on:click="startSelection" v-on:blur="handleBlur" v-model:value="namePart" type="text" class="form-control valid"></input>
                    <div v-if="showList" class="custom-select__list">
                        <div v-if="namePart">
                            <div v-if="showHasBeenAddedMessage()" class="item"><b>Этот элемент был добавлен ранее</b></div>
                            <div v-else-if="showCreateItemMessage() && (namePart && namePart.length > 2 && namePart != selectedItem.Name && !forbiddenNames.some(x => x == namePart))" v-on:click="createItem()" class="item">
                                <b>Создать</b> {{namePart}}
                            </div>
                            <div v-else-if="showNothingFoundMessage()" class="item disabled"><b>Ничего не найдено</b></div>
                        </div>
                        <div v-for='(item, itemIndex) in filteredList' v-on:click="selectItem(item)" v-if="filteredList && filteredList.length" class="item">{{item.Name}}</div>
                    </div>
               </div>`,
    props: ['input-list', "existing-item", "forbidden-names", "can-add", "isdisabled"],
    data: function () {

        return {
            showList: false,
            isSelected: false,
            namePart: null,
            selectedItem: {
                Id: 0,
                Name: null
            }

        }
    },

    mounted() {
        if (this.existingItem) {
            this.selectedItem.Id = this.existingItem.Id;
            this.selectedItem.Name = this.existingItem.Name;
            this.namePart = this.existingItem.Name;
        }
    },

    computed: {
        filteredList() {
            if (!this.namePart) {
                return this.inputList;
            }

            return this.inputList.filter(x => x.Name.toUpperCase().includes(this.namePart.toUpperCase().trim()));
        },
    },

    methods: {
        showNothingFoundMessage() {
            if (this.selectedItem.Name &&
                this.selectedItem.Name.toUpperCase() == this.namePart.toUpperCase().trim()) { // the element itself
                return false;
            }

            if (!this.filteredList || !this.filteredList.length) {
                return true;
            }

            return false;
        },

        showCreateItemMessage() {
            if (!this.canAdd) {
                return false;
            }

            if (this.selectedItem.Name &&
                this.selectedItem.Name.toUpperCase() == this.namePart.toUpperCase().trim()) { // the element itself
                return false;
            }

            if (this.forbiddenNames.filter(x => x).some(x => x.toUpperCase() == this.namePart.toUpperCase().trim())) {
                return false;
            }

            if (this.filteredList.length && this.filteredList.some(x => x.Name.toUpperCase() == this.namePart.toUpperCase().trim())) {
                return false;
            }

            return true;
        },
        showHasBeenAddedMessage() {
            if (!this.namePart || this.namePart.length <= 2) { // too early to check
                return false;
            }


            if (this.selectedItem.Name &&
                this.selectedItem.Name.toUpperCase() == this.namePart.toUpperCase().trim()) { // the element itself
                return false;
            }

            if (this.forbiddenNames && this.forbiddenNames.length &&
                this.forbiddenNames.filter(x => x)
                    .some(x => x.toUpperCase() == this.namePart.toUpperCase().trim())) {
                return true;
            }

            return false;

        },

        startSelection() {
            this.showList = true;
            this.isSelected = false;
        },

        handleBlur() {

            setTimeout(() => {
                this.showList = false;
                if (!this.isSelected) {
                    this.namePart = this.selectedItem.Name;
                }
            }, 300);
        },

        createItem() {
            this.showList = false;
            this.selectedItem.Id = 0;
            this.selectedItem.Name = this.namePart.trim();
            this.isSelected = true;
            this.$emit('itemselected', this.selectedItem)
        },

        selectItem(item) {
            this.showList = false;
            this.selectedItem.Id = item.Id;
            this.selectedItem.Name = item.Name.trim();
            this.namePart = item.Name.trim();
            this.isSelected = true;
            this.$emit('itemselected', this.selectedItem)
        }
    }
});